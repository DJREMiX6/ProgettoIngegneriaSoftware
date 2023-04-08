using CommunityToolkit.Mvvm.ComponentModel;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

[INotifyPropertyChanged]
public partial class BaseViewModel
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;
}