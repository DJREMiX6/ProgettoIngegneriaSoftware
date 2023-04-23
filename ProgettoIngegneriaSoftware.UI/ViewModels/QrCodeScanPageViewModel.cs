using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Services.QrCodeAnalyzerService;
using ProgettoIngegneriaSoftware.UI.Views;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class QrCodeScanPageViewModel : BaseViewModel
{

    private readonly IQrCodeAnalyzerService _qrCodeAnalyzerService;

    private CameraBarcodeReaderView _cameraBarcodeReaderView;

    public bool HasExited = false;

    public QrCodeScanPageViewModel(IQrCodeAnalyzerService qrCodeAnalyzerService)
    {
        _qrCodeAnalyzerService = qrCodeAnalyzerService;
        Title = "QrCode Scan";
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void OnNavigatedTo(CameraBarcodeReaderView cameraBarcodeReaderView)
    {
        _cameraBarcodeReaderView = cameraBarcodeReaderView;

        lock (_cameraBarcodeReaderView)
        {
            _cameraBarcodeReaderView.IsDetecting = true;
            _cameraBarcodeReaderView.IsEnabled = true;
        }

        HasExited = false;
    }

    [RelayCommand]
    private async Task BarCodesDetected(BarcodeResult[] barCodeResults)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            

            Int32? eventId = null;
            foreach (var barcodeResult in barCodeResults)
            {
                if (await _qrCodeAnalyzerService.IsValidQrCodeResult(barcodeResult.Value))
                {
                    eventId = await _qrCodeAnalyzerService.GetIdFromQrCodeResult(barcodeResult.Value);
                    break;
                }
            }

            if (eventId != null)
            {
                await Shell.Current.GoToAsync(nameof(EventDetailView), true, new Dictionary<string, object>()
                {
                    { "EventId", eventId },
                    { "ToFollow", true }
                });

                HasExited = true;
            }

            lock (_cameraBarcodeReaderView)
            {
                _cameraBarcodeReaderView.IsDetecting = !HasExited;
                _cameraBarcodeReaderView.IsEnabled = !HasExited;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", "An error occurred while scanning.", "Ok");
        }
        finally
        {
            IsBusy = !HasExited;
        }
    }
    
}