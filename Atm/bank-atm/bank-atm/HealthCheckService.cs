using System.Text;
using System.Text.Json;

namespace bank_atm;

public class HealthCheckService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HealthCheckService> _logger;
    private readonly string _url = "http://localhost:5843/api/health";

    public HealthCheckService(ILogger<HealthCheckService> logger)
    {
        _httpClient = new HttpClient();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var payload = new
                {
                    status = "running",
                    timestamp = DateTime.UtcNow
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_url, content, stoppingToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Health check sent correctly");
                }
                else
                {
                    _logger.LogError("Error while sending health check: {StatusCode}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on health check.");
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