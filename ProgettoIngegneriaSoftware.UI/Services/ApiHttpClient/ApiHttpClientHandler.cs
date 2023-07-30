namespace ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;

public class ApiHttpClientHandler : HttpClientHandler
{

    #region FIELDS

    private readonly CookiesService.CookiesService _cookiesService;

    #endregion FIELDS

    #region CTORS

    public ApiHttpClientHandler(CookiesService.CookiesService cookiesService)
    {
        _cookiesService = cookiesService;

        UseCookies = true;
        CookieContainer = _cookiesService;
    }

    #endregion CTORS

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = base.Send(request, cancellationToken);
        _cookiesService.SaveCookies();
        return response;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        _cookiesService.SaveCookies();
        return response;
    }

}