using Consul;

namespace Presentation.ServiceDiscovery;

internal static class ConsulExtensions
{
    public static IServiceCollection AddConsulInternal(
        this IServiceCollection services,
        ServiceConfig serviceConfig
    )
    {
        _ = services.AddSingleton(serviceConfig);

        _ = services.AddSingleton<IConsulClient, ConsulClient>(_ => new ConsulClient(config =>
            config.Address = serviceConfig.ConsulUrl
        ));

        _ = services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();

        return services;
    }
}
