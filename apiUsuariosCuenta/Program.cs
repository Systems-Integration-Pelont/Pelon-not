using apiUsuariosCuenta.authorization.concretes;
using apiUsuariosCuenta.Authorization.interfaces;
using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services;
using apiUsuariosCuenta.services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<InterUserTransactionRepository>();
builder.Services.AddScoped<UserAccessRepository>();
builder.Services.AddScoped<InterUserTransactionRepository>();
builder.Services.AddScoped<ATMInterUserTransactionRepository>();
builder.Services.AddScoped<SelfOperationRepository>();
builder.Services.AddScoped<SelfATMOperationRepository>();
builder.Services.AddScoped<InterUserTransactionTypeRepository>();
builder.Services.AddScoped<SelfOperationTypeRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<EncryptService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserAccessService, UserAccessService>();
builder.Services.AddScoped<IInterUserTransactionService, InterUserTransactionService>();
builder.Services.AddScoped<IATMInterUserTransactionService, ATMInterUserTransactionService>();
builder.Services.AddScoped<ISelfOperationService, SelfOperationService>();
builder.Services.AddScoped<ISelfATMOperationService, SelfATMOperationService>();
builder.Services.AddScoped<ISelfOperationTypeService, SelfOperationTypeService>();
builder.Services.AddScoped<IInterUserTransactionTypeService, InterUserTransactionTypeService>();
builder.Services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
  o.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank Api Team", Version = "v1" });

  OpenApiSecurityScheme securityScheme = new()
  {
    Name = "JWT Authentication",
    Description = "Enter your JWT token in this field",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = JwtBearerDefaults.AuthenticationScheme,
    BearerFormat = "JWT",
  };

  o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

  OpenApiSecurityRequirement securityRequirement = new()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = JwtBearerDefaults.AuthenticationScheme,
        },
      },
      []
    },
  };

  o.AddSecurityRequirement(securityRequirement);
});;

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "AllowAllOrigins",
    configurePolicy: policy =>
    {
      policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<BankContexts>(optionsBuilder =>
{
  var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
  optionsBuilder.UseNpgsql(connectionString);
  optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
  {
    List<Usuario> usuarios = new List<Usuario>()
    {
      new Usuario()
      {
        UserId = Usuario.Admin,
        Name = "Admin",
      },
      new Usuario()
      {
        UserId = Usuario.User,
        Name = "User",
      }
    };

    if (await context.Set<Usuario>().AnyAsync(usuario => usuario.UserId == Usuario.Admin))
    {
      return;
    }
    context.Set<Usuario>().AddRange(usuarios);
    await context.SaveChangesAsync();
  });
});


var app = builder.Build();
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
  using IServiceScope scope = app.Services.CreateScope();

  await using BankContexts dbContext =
    scope.ServiceProvider.GetRequiredService<BankContexts>();

  await dbContext.Database.MigrateAsync();
  
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
