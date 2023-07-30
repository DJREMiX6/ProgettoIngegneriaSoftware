using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoIngegneriaSoftware.UI.ViewModels;

namespace ProgettoIngegneriaSoftware.UI.Views;

public partial class RegisterPage : ContentPage
{

    #region FIELDS

    private readonly RegisterViewModel _registerViewModel;

    #endregion FIELDS

    public RegisterPage(RegisterViewModel registerViewModel)
    {
        _registerViewModel = registerViewModel;
        BindingContext = _registerViewModel;
        InitializeComponent();
    }
}