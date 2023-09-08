using Microsoft.Maui.Dispatching;
using ProgettoIngegneriaSoftware.UI.ViewModels;
using ZXing.Net.Maui;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class QrCodeScanPage : ContentPage
{

    private readonly QrCodeScanPageViewModel _viewModel;

    public QrCodeScanPage(QrCodeScanPageViewModel qrCodeScanPageViewModel)
    {
        _viewModel = qrCodeScanPageViewModel;
        BindingContext = _viewModel;
        InitializeComponent();
        SetupQrCodeReader();
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
        _viewModel.OnNavigatedTo(cameraBarcodeReaderView);
    }

    public void CameraBarcodeReaderView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        cameraBarcodeReaderView.IsDetecting = false;
        cameraBarcodeReaderView.IsEnabled = false;

        Dispatcher.DispatchAsync(async () =>
        {
            await _viewModel.BarCodesDetected(e.Results);
        });
    }

}