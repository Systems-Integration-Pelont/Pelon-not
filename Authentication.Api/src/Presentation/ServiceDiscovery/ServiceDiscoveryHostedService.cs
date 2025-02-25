using Consul;

namespace Presentation.ServiceDiscovery;

internal sealed class ServiceDiscoveryHostedService(
    ILogger<ServiceDiscoveryHostedService> logger,
    IConsulClient consulClient,
    ServiceConfig serviceConfig
) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        AgentServiceRegistration registration = new()
        {
            ID = serviceConfig.Id,
            Name = serviceConfig.Name,
            Address = serviceConfig.ApiHost,
            Port = serviceConfig.Port,

            Check = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                Interval = TimeSpan.FromSeconds(15),
                HTTP = $"http://{serviceConfig.ApiHost}:{serviceConfig.Port}/health",
                Timeout = TimeSpan.FromSeconds(5),
            },
        };

        try
        {
            _ = await consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            _ = await consulClient.Agent.ServiceRegister(registration, cancellationToken);

            logger.LogInformation("Service registered with Consul");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to register service with Consul");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _ = await consulClient.Agent.ServiceDeregister(serviceConfig.Id, cancellationToken);

            logger.LogInformation("Service deregistered with Consul");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to deregister service with Consul");
        }
    }
}
