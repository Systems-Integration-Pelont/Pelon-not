using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Presentation.Extensions;
using Presentation.Infrastructure;
using Presentation.Services;

namespace Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddCorsInternal();

        services.AddOcelot(configuration).AddConsul<ConsulBuilder>().AddPolly();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerForOcelot(configuration);

        services.AddAuthorization();

        return services
            .AddServicesInternal()
            .AddAuthenticationInternal(configuration)
            .AddCacheInternal(configuration);
    }

    private static IServiceCollection AddServicesInternal(this IServiceCollection services)
    {
        services.AddScoped<CacheService>();

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)
                    ),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        CacheService cacheService =
                            context.HttpContext.RequestServices.GetRequiredService<CacheService>();

                        string token = context
                            .Request.Headers.Authorization.ToString()
                            .Replace("Bearer ", string.Empty);

                        if (await cacheService.IsTokenBlacklistedAsync(token))
                        {
                            context.Fail("Token is blacklisted.");
                        }
                    },
                };
            });

        return services;
    }

    private static IServiceCollection AddCacheInternal(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");

            options.InstanceName = configuration["Cache:InstanceName"];
        });

        return services;
    }
}
