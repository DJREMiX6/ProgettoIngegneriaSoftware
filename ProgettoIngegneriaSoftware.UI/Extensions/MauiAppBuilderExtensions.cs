using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.UI.Services.Extensions;
using ProgettoIngegneriaSoftware.UI.ViewModels;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.Extensions;

internal static class MauiAppBuilderExtensions
{
    internal static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services
            .AddEventsService()
            .AddQrCodeAnalyzerService();

        return builder;
    }

    internal static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<AppShellViewModel>()

            .AddSingleton<EventsPage>()
            .AddSingleton<EventsViewModel>()

            .AddTransient<EventDetailView>()
            .AddTransient<EventDetailViewModel>()
            
            .AddTransient<QrCodeScanPage>()
            .AddTransient<QrCodeScanPageViewModel>()
            
            .AddTransient<QrCodeDetailView>()
            .AddTransient<QrCodeDetailViewModel>();

        return builder;
    }

}