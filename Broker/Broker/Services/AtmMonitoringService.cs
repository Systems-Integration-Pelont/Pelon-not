using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Broker.Services
{
    public class AtmMonitoringService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AtmMonitoringService> _logger;
        private readonly string _atmServiceUrl;
        private readonly string _emailServiceUrl;
        private readonly string _fromEmail;
        private readonly string _emailPassword;

        public AtmMonitoringService(ILogger<AtmMonitoringService> logger, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _atmServiceUrl = configuration["AtmServiceUrl"];
            _emailServiceUrl = configuration["EmailServiceUrl"];
            _fromEmail = configuration["EmailCredentials:From"];
            _emailPassword = configuration["EmailCredentials:Password"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var atmResponse = await _httpClient.GetAsync($"{_atmServiceUrl}/Atm/health", stoppingToken);
                    if (!atmResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError("Error al obtener el estado del ATM");
                        continue;
                    }

                    var atmStatus = await atmResponse.Content.ReadAsStringAsync();

                    var emailPayload = new
                    {
                        FromMail = _fromEmail,
                        AppPassword = _emailPassword,
                        ToMail = "destino@ejemplo.com",
                        Affair = "Estado de ATMs",
                        Content = $"Estado actual del ATM: {atmStatus}"
                    };

                    var emailContent = new StringContent(JsonSerializer.Serialize(emailPayload), Encoding.UTF8, "application/json");
                    var emailResponse = await _httpClient.PostAsync($"{_emailServiceUrl}/Pop3/sendMail", emailContent, stoppingToken);

                    if (emailResponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Correo enviado correctamente con el estado del ATM.");
                    }
                    else
                    {
                        _logger.LogError("Error al enviar el correo con el estado del ATM.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en el monitoreo del ATM.");
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

        public override void Dispose()
        {
            _httpClient.Dispose();
            base.Dispose();
        }
    }
}
