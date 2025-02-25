namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsInternal(this IServiceCollection services)
    {
        return services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            )
        );
    }
}
