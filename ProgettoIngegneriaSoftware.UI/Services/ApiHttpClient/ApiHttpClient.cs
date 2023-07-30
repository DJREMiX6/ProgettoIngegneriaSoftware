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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginUserInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedApiException"></exception>
    /// <exception cref="BadRequestApiException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task SignInAsync(ILoginUserInfo loginUserInfo, CancellationToken? cancellationToken = null)
    {
        var uri = _uriService.AuthenticationSignInPath();
        var httpResponseMessage = await this.PostAsJsonAsync(uri, loginUserInfo,
            cancellationToken ?? new CancellationToken(false));

        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        if(httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
        {
            var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new UnauthorizedApiException(problemDetails?.Detail);
        }
        if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            var problemDetails = await httpResponseMessage.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new BadRequestApiException(problemDetails?.Detail);
        }

        throw new ArgumentException($"Unexpected error! Status code: {httpResponseMessage.StatusCode}.", nameof(httpResponseMessage.StatusCode));
    }
}