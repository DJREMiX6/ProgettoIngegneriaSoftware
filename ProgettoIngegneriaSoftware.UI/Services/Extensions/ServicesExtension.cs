using ProgettoIngegneriaSoftware.UI.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.UI.Services.CookiesService;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Services.HttpClientFactory;
using ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;
using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.Extensions;

internal static class ServicesExtension
{

    internal static IServiceCollection AddEventsService(this IServiceCollection services) =>
        services.AddSingleton<IEventsService, EventsService.EventService>();

    internal static IServiceCollection AddQrCodeAnalyzerService(this IServiceCollection services) =>
        services.AddTransient<IQrCodeAnalyzerService, QrCodeLocalAnalyzerService>();

    internal static IServiceCollection AddAuthenticationService(this IServiceCollection services) =>
        services.AddSingleton<IAuthenticationService, AuthenticationService.AuthenticationService>();

    internal static IServiceCollection AddUriService(this IServiceCollection services) =>
        services.AddTransient<IUriService, UriService.UriService>();

    internal static IServiceCollection AddProgettoIngegneriaSoftwareApiHttpClientFactory(this IServiceCollection services) => 
        services.AddSingleton<ProgettoIngegneriaSoftwareApiHttpClientFactory>();

    internal static IServiceCollection AddCookiesService(this IServiceCollection services) =>
        services.AddSingleton<ICookiesService, CookiesService.CookiesService>();

}