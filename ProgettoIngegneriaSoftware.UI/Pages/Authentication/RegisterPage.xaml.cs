using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.UI.Services.RegisterUserService;

namespace ProgettoIngegneriaSoftware.UI.Pages;

public partial class RegisterPage : ContentPage
{

    #region PRIVATE FIELDS
    
    private readonly IRegisterUserService _registerUserService;

    #endregion PRIVATE FIELDS

    #region CTORS

    public RegisterPage(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
        InitializeComponent();
    }

    #endregion CTORS

    #region EVENT HANDLERS

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        var userModel = await CreateUserModelFromInput();
        if (userModel != null)
        {
            var httpStatusCode = _registerUserService.RegisterUser(userModel);
        }
    }

    #endregion EVENT HANDLERS

    #region PRIVATE METHODS

    private async Task<UserModelRecord?> CreateUserModelFromInput()
    {
        return new UserModelRecord()
        {
            Username = usernameEntry.Text.Trim(),
            Password = passwordEntry.Text,
            ConfirmPassword = confirmPasswordEntry.Text,
            Email = emailEntry.Text.Trim()
        };
    }

    #endregion PRIVATE METHODS

}