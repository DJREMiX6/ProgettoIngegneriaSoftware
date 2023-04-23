using ProgettoIngegneriaSoftware.UI.Extensions;
using ZXing.Net.Maui.Controls;

#if ANDROID
[assembly: Android.App.UsesPermission(Android.Manifest.Permission.Camera)]
#endif

namespace ProgettoIngegneriaSoftware.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterServices()
                .UseBarcodeReader()
                .AddPages()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}