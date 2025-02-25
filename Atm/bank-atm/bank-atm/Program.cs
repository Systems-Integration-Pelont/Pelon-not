using bank_atm;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHostedService<HealthCheckService>();
var app = builder.Build();


app.MapControllers();

app.Run();