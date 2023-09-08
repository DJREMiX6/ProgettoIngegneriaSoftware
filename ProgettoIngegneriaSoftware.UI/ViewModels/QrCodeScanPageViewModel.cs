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
    
    public void OnNavigatedTo(CameraBarcodeReaderView cameraBarcodeReaderView)
    {
        _cameraBarcodeReaderView = cameraBarcodeReaderView;

        lock (_cameraBarcodeReaderView)
        {
            _cameraBarcodeReaderView.IsDetecting = true;
            _cameraBarcodeReaderView.IsEnabled = true;
        }

        HasExited = false;
    }
    
    public async Task BarCodesDetected(BarcodeResult[] barCodeResults)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            

            Guid eventId = Guid.Empty;
            foreach (var barcodeResult in barCodeResults)
            {
                eventId = await _qrCodeAnalyzerService.GetIdFromQrCodeResult(barcodeResult.Value);
                if (eventId != Guid.Empty)
                    break;
            }

            if (eventId != Guid.Empty)
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