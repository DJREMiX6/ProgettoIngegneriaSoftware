using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{

    private readonly QrCodeScanPage _qrCodeScanPage;

    public AppShellViewModel(QrCodeScanPage qrCodeScanPage)
    {
        _qrCodeScanPage = qrCodeScanPage;
    }

    [RelayCommand]
    private async Task OpenQrCodeScanPageAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(_qrCodeScanPage, true);
    }

}