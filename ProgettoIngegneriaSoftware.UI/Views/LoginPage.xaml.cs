using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class LoginPage : ContentPage
{

    #region FIELDS

    private readonly LoginViewModel _loginViewModel;

    #endregion FIELDS

    public LoginPage(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
        BindingContext = _loginViewModel;
        InitializeComponent();
    }
}