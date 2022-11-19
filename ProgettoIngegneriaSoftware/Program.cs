using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Events;
using ProgettoIngegneriaSoftware.Extensions;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(".log")
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.development.json");
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Services.AddDatabaseContexts(builder.Configuration, useTempDb: true);
builder.Services.AddServices();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDefaultAuthenticatedUser();
}

/*app.UseCustomAuthentication();*/
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting web host");
    app.Run();
}
catch(Exception ex)
{
    Log.Error(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
