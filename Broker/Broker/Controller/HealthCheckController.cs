using System.Text;
using System.Text.Json;
using Broker.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Broker.Controllers;

[ApiController]
[Route("api/broker/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _emailServiceUrl;
    private readonly IConfiguration _configuration;

    public HealthController(
        ILogger<HealthController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _configuration = configuration;
        _emailServiceUrl = configuration["EmailServiceUrl"];
    }

    [HttpPost("report")]
    public async Task<IActionResult> ReceiveAtmStatus([FromBody] AtmStatus status)
    {
        try
        {
            var emailPayload = new SendMailDTO
            {
                FromMail = _configuration["EmailCredentials:From"],
                AppPassword = _configuration["EmailCredentials:Password"],
                ToMail = "admin@banco.com",
                Affair = $"Estado ATM - {status.AtmId}",
                Content = $"ATM ID: {status.AtmId}\n" +
                         $"Location: {status.Location}\n" +
                         $"Status: {status.Status}\n" +
                         $"Last Checked: {status.LastChecked}"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(emailPayload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                $"{_emailServiceUrl}/Pop3/sendMail",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error al enviar email: {StatusCode}", response.StatusCode);
                return StatusCode(500, "Error al procesar el estado del ATM");
            }

            return Ok("Estado del ATM procesado correctamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al procesar el estado del ATM");
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
