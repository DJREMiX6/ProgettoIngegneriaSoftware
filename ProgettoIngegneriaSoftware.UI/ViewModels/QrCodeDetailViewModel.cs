using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[QueryProperty("EventId", "EventId")]
public partial class QrCodeDetailViewModel : BaseViewModel
{

    [ObservableProperty]
    private int _eventId;

    public QrCodeDetailViewModel()
    {
        Title = "QrCode Detail";
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}