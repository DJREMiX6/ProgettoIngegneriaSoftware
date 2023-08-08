using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Exceptions.API;
using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;

public class ApiHttpClient : HttpClient
{

    #region FIELDS

    private readonly IUriService _uriService;

    #endregion FIELDS

    #region CTORS

    public ApiHttpClient(IUriService uriService, HttpClientHandler httpClientHandler) : base(httpClientHandler)
    {
        _uriService = uriService;
    }

    #endregion CTORS
    
    public async Task SignInAsync(ILoginUserInfo loginUserInfo, CancellationToken? cancellationToken = null)
    {
        var uri = _uriService.AuthenticationSignInPath();
        var httpResponseMessage = await this.PostAsJsonAsync(uri, loginUserInfo,
            cancellationToken ?? new CancellationToken(false));

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return;
        }
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new UnauthorizedApiException(problemDetails?.Detail);
            }
            case HttpStatusCode.BadRequest:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new BadRequestApiException(problemDetails?.Detail);
            }
            default:
                throw new ArgumentException($"Unexpected error! Status code: {httpResponseMessage.StatusCode}.", nameof(httpResponseMessage.StatusCode));
        }
    }

    public async Task SignUpAsync(IRegisterUserInfo registerUserInfo, CancellationToken? cancellationToken = null)
    {
        var uri = _uriService.AuthenticationSignUpPath();
        var httpResponseMessage = await this.PostAsJsonAsync(uri, registerUserInfo, 
            cancellationToken ?? new CancellationToken(false));

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return;
        }
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new UnauthorizedApiException(problemDetails?.Detail);
            }
            case HttpStatusCode.BadRequest:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new BadRequestApiException(problemDetails?.Detail);
            }
            default:
                throw new ArgumentException($"Unexpected error! Status code: {httpResponseMessage.StatusCode}.", nameof(httpResponseMessage.StatusCode));
        }
    }

    public async Task SignOut(CancellationToken? cancellationToken = null)
    {
        var uri = _uriService.AuthenticationSignOutPath();
        var httpResponseMessage = await this.PostAsync(uri, null, cancellationToken ?? new CancellationToken(false));

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return;
        }
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new UnauthorizedApiException(problemDetails?.Detail);
            }
            case HttpStatusCode.BadRequest:
            {
                var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new BadRequestApiException(problemDetails?.Detail);
            }
            default:
                throw new ArgumentException($"Unexpected error! Status code: {httpResponseMessage.StatusCode}.", nameof(httpResponseMessage.StatusCode));
        }
    }

}