using System.Net.Http.Json;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.HttpClientFactory;

public class ProgettoIngegneriaSoftwareApiHttpClient : HttpClient
{

    #region FIELDS

    private readonly IUriService _uriService;

    #endregion FIELDS

    #region CTORS

    public ProgettoIngegneriaSoftwareApiHttpClient(IUriService uriService)
    {
        _uriService = uriService;
        _cookies = new Dictionary<string, string>();
    }

    #endregion CTORS

    public async Task AuthenticationSignIn(ILoginUserInfo loginUserInfo, CancellationToken? cancellationToken = null)
    {
        var httpResponseMessage = await this.PostAsJsonAsync(_uriService.AuthenticationSignInPath(), loginUserInfo,
            cancellationToken ?? new CancellationToken(false));

        if(httpResponseMessage)
    }
}