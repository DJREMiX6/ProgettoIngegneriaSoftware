using ProgettoIngegneriaSoftware.API.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.API.Services.RegistrationService;
using ProgettoIngegneriaSoftware.API.Services.UserEntityModelService;

namespace ProgettoIngegneriaSoftware.API.Services.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddRegistrationService(this IServiceCollection services) =>
        services.AddScoped<IRegistrationService, RegistrationService.RegistrationService>();

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services) =>
        services.AddScoped<IAuthenticationService, CookieBasedAuthenticationService>();

    public static IServiceCollection AddModelsServices(this IServiceCollection services) =>
        services.AddUserEntityModelService();

    private static IServiceCollection AddUserEntityModelService(this IServiceCollection services) =>
        services.AddScoped<IUserEntityModelService, EntityFrameworkUserEntityModelService>();
}