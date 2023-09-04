using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Exceptions.API;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    #region FIELDS

    private readonly ApiHttpClient _apiHttpClient;

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

    public RegisterViewModel(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    #endregion CTORS

    #region COMMANDS

    [RelayCommand]
    private async Task Register()
    {
        IsBusy = true;
        var redirect = false;
        IToast toast;
        try
        {
            var registerUserInfo = new RegisterUserInfo()
            {
                UserName = Username,
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
            await _apiHttpClient.SignUpAsync(registerUserInfo);
            toast = Toast.Make("User registered correctly. Now login.", ToastDuration.Long, 18d);
            redirect = true;
        }
        catch (UnauthorizedApiException unauthorizedApiException)
        {
            redirect = true;
            toast = Toast.Make(unauthorizedApiException.Message, ToastDuration.Long, textSize: 18d);
        }
        catch (BadRequestApiException badRequestApiException)
        {
            toast = Toast.Make(badRequestApiException.Message, ToastDuration.Long, textSize: 18d);
        }
        catch (Exception exception)
        {
#if DEBUG
            toast = Toast.Make(exception.Message, ToastDuration.Long, textSize: 18d);
#else
            toast = Toast.Make("Unknown error.", ToastDuration.Long, textSize: 18d);
#endif
        }

        await toast.Show();

        if (redirect)
            await NavigateToLoginPage();

        IsBusy = false;
    }

    [RelayCommand]
    private async Task NavigateToLoginPage()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("..", animate: true);
        IsBusy = false;
    }

    #endregion COMMANDS
}