using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.UI.Services.Extensions;
using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.UI.Pages;
using ProgettoIngegneriaSoftware.UI.ViewModels;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.Extensions;

internal static class MauiAppBuilderExtensions
{
    internal static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services
            .AddRegisterUserService()
            .AddUriProviderService()
            .AddEventsService();

        return builder;
    }

    internal static MauiAppBuilder AddConfiguration(this MauiAppBuilder builder)
    {
        var openAppPackageFileAsyncTask = FileSystem.OpenAppPackageFileAsync("appsettings.json");
        Task.WaitAll(openAppPackageFileAsyncTask);
        var appsettingsStream = openAppPackageFileAsyncTask.Result;

        /*using var reader = new StreamReader(appsettingsStream);
        var contents = reader.ReadToEnd();*/

        builder.Configuration.AddJsonStream(appsettingsStream);
        return builder;
    }

    internal static MauiAppBuilder AddPages(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<EventsPage>()
            .AddSingleton<EventsViewModel>()
            .AddSingleton<TestPage>()
            .AddSingleton<RegisterPage>()
            .AddSingleton<QrCodeScanPage>();

        return builder;
    }

}