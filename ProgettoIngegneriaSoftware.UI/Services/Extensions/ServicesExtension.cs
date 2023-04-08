using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Services.RegisterUserService;
using ProgettoIngegneriaSoftware.UI.Services.UriProviderService;

namespace ProgettoIngegneriaSoftware.UI.Services.Extensions;

internal static class ServicesExtension
{

    internal static IServiceCollection AddRegisterUserService(this IServiceCollection services) =>
        services.AddSingleton<IRegisterUserService, RegisterUserService.RegisterUserService>();

    internal static IServiceCollection AddUriProviderService(this IServiceCollection services) =>
        services.AddTransient<IUriProviderService, UriProviderService.UriProviderService>();

    internal static IServiceCollection AddEventsService(this IServiceCollection services) =>
        services.AddTransient<IEventsService, EventsService.EventsService>();

}