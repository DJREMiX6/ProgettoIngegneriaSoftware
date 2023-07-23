namespace ProgettoIngegneriaSoftware.UI.Services.CookiesService;

public interface ICookiesService
{
    public IReadOnlyDictionary<string, string> Cookies { get; }
    public ICookiesService SetCookie(string key, string value);
}