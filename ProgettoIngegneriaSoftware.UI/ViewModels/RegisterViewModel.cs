using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    #region FIELDS

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmPassword;

    #endregion FIELDS

    #region CTORS

    public RegisterViewModel()
    {
    }

    #endregion CTORS

    #region COMMANDS

    [RelayCommand]
    private async Task Register()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task NavigateToLoginPage()
    {
        await Shell.Current.GoToAsync("..", animate: true);
    }

    #endregion COMMANDS
}