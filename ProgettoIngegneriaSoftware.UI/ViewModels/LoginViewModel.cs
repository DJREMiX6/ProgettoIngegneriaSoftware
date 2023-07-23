using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Services.AuthenticationService;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class LoginViewModel : BaseViewModel
{

    #region FIELDS

    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    #endregion FIELDS

    #region CTORS

    public LoginViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        Title = "Sign In";
    }

    #endregion CTORS

    #region COMMANDS

    [RelayCommand]
    private async Task Login()
    {
        await Toast.Make("TestMessage", ToastDuration.Long, textSize: 24d).Show();

    }

    #endregion COMMANDS

}