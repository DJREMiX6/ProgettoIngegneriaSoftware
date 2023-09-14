using System.Net;
using Newtonsoft.Json;
using ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;
using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;
using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.Extensions;

internal static class ServicesExtension
{

    internal static IServiceCollection AddEventsService(this IServiceCollection services) =>
        services.AddSingleton<IEventsService, EventsService.EventService>();

    internal static IServiceCollection AddQrCodeAnalyzerService(this IServiceCollection services) =>
        services.AddTransient<IQrCodeAnalyzerService, QrCodeAnalyzerService.QrCodeAnalyzerService>();

    internal static IServiceCollection AddUriService(this IServiceCollection services) =>
        services.AddTransient<IUriService, UriService.UriService>();

    private static IServiceCollection AddApiHttpClient(this IServiceCollection services) => 
        services.AddSingleton<ApiHttpClient.ApiHttpClient>();

    private static IServiceCollection AddCookiesService(this IServiceCollection services) =>
        services.AddSingleton<CookiesService.CookiesService>();

    private static IServiceCollection AddApiHttpClientHandler(this IServiceCollection services) => 
        services.AddSingleton<HttpClientHandler, ApiHttpClientHandler>();

    internal static IServiceCollection AddHttpClient(this IServiceCollection services) =>
        services
            .AddApiHttpClient()
            .AddApiHttpClientHandler()
            .AddCookiesService();

    internal static IServiceCollection AddEventBookingService(this IServiceCollection services) =>
        services.AddSingleton<IEventBookingService, EventBookingService>();

    internal static IServiceCollection AddJsonSerializerSettings(this IServiceCollection services) =>
        services.AddSingleton<JsonSerializerSettings, CustomJsonSerializerSettings.CustomJsonSerializerSettings>();

}