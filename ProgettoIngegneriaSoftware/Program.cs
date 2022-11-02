using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Extensions;
using ProgettoIngegneriaSoftware.Models;

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
builder.Services.AddDatabaseContext();
builder.Services.AddServices();
builder.Services.AddPasswordHasher();
builder.Services.AddModels();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
