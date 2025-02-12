namespace Presentation.ServiceDiscovery;

internal static class ServiceConfigExtensions
{
    public static ServiceConfig GetServiceConfig(this IConfiguration configuration)
    {
        ServiceConfig serviceConfig = new()
        {
            Id = $"{configuration.GetValue<string>("Consul:Id")!}{Guid.NewGuid()}",
            Name = configuration.GetValue<string>("Consul:Name")!,
            ConsulUrl = configuration.GetValue<Uri>("ServiceDiscovery:Url")!,
            Port = configuration.GetValue<int>("ServiceDiscovery:Port"),
            ApiHost = configuration.GetValue<string>("ServiceDiscovery:ApiHost")!,
        };

        return serviceConfig;
    }
}
