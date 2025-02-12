namespace API.DTO;

public class SendMailDTO
{
    public string ToMail { get; set; }
    public string Content { get; set; }
    public string Affair { get; set; }
    public string FromMail { get; set; }
    public string AppPassword { get; set; }
}