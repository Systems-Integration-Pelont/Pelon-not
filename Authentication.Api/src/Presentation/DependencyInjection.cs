using Presentation.Extensions;
using Presentation.Infrastructure;
using Presentation.ServiceDiscovery;

namespace Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddEndpointsApiExplorer();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddSignalR();

        services.AddRateLimiterInternal(configuration);

        services.AddConsulInternal(configuration.GetServiceConfig());

        return services;
    }
}
