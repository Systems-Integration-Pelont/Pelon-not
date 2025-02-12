using API.DTO;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Pop3Controller: ControllerBase
{
    [HttpPost("sendMail")]
    public async Task<IActionResult> SendMail(SendMailDTO mailDto)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("sender", mailDto.FromMail));
        message.To.Add(new MailboxAddress("receiver", mailDto.ToMail));
        message.Subject = mailDto.Affair;

        message.Body = new TextPart("plain")
        {
            Text = mailDto.Content
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(mailDto.FromMail, mailDto.AppPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return Ok();
    }

    [HttpPost("getMails")]
    public async Task<IActionResult> GetMails(GetMailsDTO mailsDto)
    {
        var client = new ImapClient();

        try
        {
            await client.ConnectAsync("outlook.office365.com", 995, MailKit.Security.SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(mailsDto.Mail, mailsDto.Password);

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);

            var uids = await inbox.SearchAsync(SearchQuery.All);
            var messages = new List<MimeMessage>();

            for (int i = Math.Max(0, uids.Count - 5); i < uids.Count; i++)
            {
                messages.Add(await inbox.GetMessageAsync(uids[i]));
            }

            await client.DisconnectAsync(true);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error obtaining the mails: {ex.Message}");
        }
    }
    
}