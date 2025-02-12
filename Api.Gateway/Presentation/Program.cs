using Ocelot.Middleware;
using Presentation;
using Presentation.Configurations;
using Presentation.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration)
);

builder.Configuration.AddJsonFile(
    Constants.OcelotConfiguration,
    optional: false,
    reloadOnChange: true
);

builder.Services.AddPresentation(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(options =>
    {
        options.PathToSwaggerGenerator = Constants.SwaggerDocs;
        options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
    });
}

app.UseCors();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

await app.UseOcelot();

await app.RunAsync();
