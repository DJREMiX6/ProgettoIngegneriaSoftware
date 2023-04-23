using ProgettoIngegneriaSoftware.UI.Services.EventsService;
using ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;

namespace ProgettoIngegneriaSoftware.UI.Services.Extensions;

internal static class ServicesExtension
{

    internal static IServiceCollection AddEventsService(this IServiceCollection services) =>
        services.AddSingleton<IEventsService, EventsService.EventsServiceLocal>();

    internal static IServiceCollection AddQrCodeAnalyzerService(this IServiceCollection services) =>
        services.AddTransient<IQrCodeAnalyzerService, QrCodeLocalAnalyzerService>();

}