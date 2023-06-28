using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API;
using ProgettoIngegneriaSoftware.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddConfiguredServices(builder.Configuration);

var app = builder.Build();

//Ensure Database creation before starting the application
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
