namespace ProgettoIngegneriaSoftware.UI.Services.CookiesService.Extensions;

public static class CookieServiceExtensions
{

    private const string AUTHENTICATION_TOKEN_COOKIE = "AuthenticationToken";

    public static string GetApiAuthenticationCookie(this ICookiesService cookiesService) => cookiesService.Cookies[AUTHENTICATION_TOKEN_COOKIE];
    public static ICookiesService SetApiAuthenticationCookie(this ICookiesService cookiesService, string value) => cookiesService.SetCookie(AUTHENTICATION_TOKEN_COOKIE, value);
}