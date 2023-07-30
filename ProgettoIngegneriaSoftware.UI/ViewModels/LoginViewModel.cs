using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProgettoIngegneriaSoftware.UI.Exceptions.API;
using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;
using ProgettoIngegneriaSoftware.UI.Views;

namespace ProgettoIngegneriaSoftware.UI.ViewModels;

public partial class LoginViewModel : BaseViewModel
{

    #region FIELDS
    
    private readonly ApiHttpClient _httpClient;

    [ObservableProperty]
    private string _email = "pisello@palle.com"; //TODO REMOVE

    [ObservableProperty]
    private string _password = "PiselloPalle22!"; //TODO REMOVE

    #endregion FIELDS

    #region CTORS

    public LoginViewModel(ApiHttpClient httpClient)
    {
        _httpClient = httpClient;
        Title = "Sign In";
    }

    #endregion CTORS

    #region COMMANDS

    [RelayCommand]
    private async Task Login()
    {
        if (IsBusy) return;

        IsBusy = true;

        IToast? toast;
        try
        {
            var loginUserInfo = new LoginUserInfo()
            {
                Email = Email,
                Password = Password
            };

            await _httpClient.SignInAsync(loginUserInfo);
            toast = Toast.Make("Successfully signed in.", ToastDuration.Long, textSize: 18d);
        }
        catch (UnauthorizedApiException unauthorizedApiException)
        {
            toast = Toast.Make(unauthorizedApiException.Message, ToastDuration.Long, textSize: 18d);
        }
        catch (BadRequestApiException badRequestApiException)
        {
            toast = Toast.Make(badRequestApiException.Message, ToastDuration.Long, textSize: 18d);
        }
        /*catch (Exception exception)
        {
            toast = Toast.Make(exception.Message, ToastDuration.Long, textSize: 18d);
        }*/

        await toast.Show();

        IsBusy = false;
    }

    [RelayCommand] 
    private async Task NavigateToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage), animate: true);
    }

    #endregion COMMANDS

}