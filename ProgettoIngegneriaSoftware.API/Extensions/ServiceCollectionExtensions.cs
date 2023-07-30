using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API.Security;
using ProgettoIngegneriaSoftware.API.Security.Argon2;
using ProgettoIngegneriaSoftware.API.Services.Extensions;

namespace ProgettoIngegneriaSoftware.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDbContext(configuration)
            .AddRegistrationService()
            .AddAuthentication()
            .AddModelsServices()
            .AddPasswordHasher();

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetApplicationConnectionString()));

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthenticationService()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(28);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
            });

        return services;
    }

    private static IServiceCollection AddPasswordHasher(this IServiceCollection services) =>
        services
            .AddTransient<IPasswordHasher, Argon2PasswordHasher>()
            .AddTransient<IPasswordHasherOptions, Argon2PasswordHasherOptions>();

}