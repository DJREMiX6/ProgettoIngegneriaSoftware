using System.Net;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProgettoIngegneriaSoftware.UI.Services.CookiesService;

public class CookiesService : CookieContainer
{

    #region FIELDS
    
    private const string COOKIES_FILE_NAME = "biscuits.json";

    private string? _cookiesFilePath;

    #endregion FIELDS

    #region CTORS

    public CookiesService()
    {
        _cookiesFilePath = Path.Combine(FileSystem.CacheDirectory, COOKIES_FILE_NAME);
        LoadCookies();
    }

    #endregion CTORS

    #region METHODS

    public void LoadCookies()
    {
        if (!File.Exists(_cookiesFilePath))
        {
            return;
        }

        var cookieFileContent = File.ReadAllText(_cookiesFilePath);
        if (string.IsNullOrWhiteSpace(cookieFileContent))
            return;
        var deserializedCookies = JsonSerializer.Deserialize<CookieCollection>(cookieFileContent);

        if (deserializedCookies == null) return;

        foreach(var cookie in deserializedCookies.ToArray())
        {
            var cookieCopy = new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
            {
                HttpOnly = cookie.HttpOnly,
                Comment = cookie.Comment,
                CommentUri = cookie.CommentUri,
                Discard = cookie.Discard,
                Expired = cookie.Expired,
                Expires = cookie.Expires,
                Secure = cookie.Secure,
                Version = cookie.Version,
                /* If set, this property is not making the
                     * HttpClientHandler to send the cookie loaded from the cookies file */
                /* Port = cookie.Port,*/
            };
            Add(cookieCopy);
        }
    }

    public void SaveCookies()
    {
        var cookies = GetAllCookies();

        if (cookies.Count <= 0) return;

        var serializedCookies = JsonSerializer.Serialize(cookies);
        File.WriteAllText(_cookiesFilePath, serializedCookies);
    }

    #endregion METHODS

}