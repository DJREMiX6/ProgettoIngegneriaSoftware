using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API;
using ProgettoIngegneriaSoftware.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddConfiguredServices(builder.Configuration)
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

//Ensure Database creation before starting the application
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var migrated = false;
    var attempts = 0;
    var maxAttempts = 3;
    
    while(!migrated || attempts < maxAttempts)
    {
        try
        {
            attempts++;
            logger.LogInformation("Attempting to migrate the Database. Attempt {attempts}/{maxAttempts}", attempts, maxAttempts);
            db.Migrate();
            migrated = true;
        }
        catch (SqlException)
        {
            logger.LogError("Migration failed. Attempt {attempts}/{maxAttempts}", attempts, maxAttempts);
            logger.LogInformation("Waiting 10s for a new attempt.");
            migrated = false;
            Thread.Sleep(10_000);
        }
    }

    if(attempts > maxAttempts && !migrated)
    {
        logger.LogError("Max attempts reached. Shutting down.");
        return -1;
    }
    if(migrated)
        logger.LogInformation("Migration successful.");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Ok");

app.Run();
