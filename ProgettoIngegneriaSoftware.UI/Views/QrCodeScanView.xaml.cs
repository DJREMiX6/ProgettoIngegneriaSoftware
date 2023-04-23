using Microsoft.Maui.Dispatching;
using ProgettoIngegneriaSoftware.UI.ViewModels;
using ZXing.Net.Maui;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class QrCodeScanPage : ContentPage
{
    public QrCodeScanPage(QrCodeScanPageViewModel qrCodeScanPageViewModel)
    {
        InitializeComponent();
        SetupQrCodeReader();

        BindingContext = qrCodeScanPageViewModel;
    }

    private void SetupQrCodeReader()
    {
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions()
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true,
            TryInverted = true
        };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        (BindingContext as QrCodeScanPageViewModel)!.NavigatedToCommand.Execute(cameraBarcodeReaderView);
    }

    public void CameraBarcodeReaderView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        cameraBarcodeReaderView.IsDetecting = false;
        cameraBarcodeReaderView.IsEnabled = false;
        var context = (BindingContext as QrCodeScanPageViewModel)!;
        Dispatcher.DispatchAsync(async () =>
        {
            await context.BarCodesDetectedCommand.ExecuteAsync(e.Results);
        });
    }

}