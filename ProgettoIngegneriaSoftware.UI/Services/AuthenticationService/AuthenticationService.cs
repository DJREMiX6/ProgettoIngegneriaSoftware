using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Exceptions;
using ProgettoIngegneriaSoftware.UI.Services.HttpClientFactory;
using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{

    #region FIELDS

    private readonly ProgettoIngegneriaSoftwareApiHttpClientFactory _httpClientFactory;

    #endregion FIELDS

    #region CTORS

    public AuthenticationService(ProgettoIngegneriaSoftwareApiHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    #endregion CTORS

    #region IAuthenticationService IMPLEMENTATION

    public bool IsUserAuthenticated() => false;

    public async Task SignInAsync(ILoginUserInfo loginUserInfo)
    {
        using (var client = _httpClientFactory.Make())
        {
            try
            {
                await client.AuthenticationSignIn(loginUserInfo);
            }
            catch (AuthenticationException authenticationException)
            {

            }
        }
    }

    #endregion IAuthenticationService IMPLEMENTATION

}