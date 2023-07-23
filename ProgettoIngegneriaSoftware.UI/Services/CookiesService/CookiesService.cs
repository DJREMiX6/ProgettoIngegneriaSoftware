using System.Text.Json;
using Newtonsoft.Json;

namespace ProgettoIngegneriaSoftware.UI.Services.CookiesService;

public class CookiesService : ICookiesService
{

    #region FIELDS

    private Dictionary<string, string>? _cookiesDictionary;
    private const string COOKIES_FILE_NAME = "biscuits.js";

    private string? _cookiesFilePath;

    #endregion FIELDS

    #region CTORS

    public CookiesService()
    {
        _cookiesFilePath = Path.Combine(FileSystem.CacheDirectory, COOKIES_FILE_NAME);
    }

    #endregion CTORS

    #region PROPS

    private Dictionary<string, string> CookiesDictionary => _cookiesDictionary ??= new Dictionary<string, string>();

    public IReadOnlyDictionary<string, string> Cookies => CookiesDictionary;

    #endregion PROPS

    #region METHODS

    public ICookiesService SetCookie(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException(key);
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(value);

        CookiesDictionary.Add(key, value);

        return this;
    }

    private async Task LoadCookies()
    {
        if (!File.Exists(_cookiesFilePath))
        {
            File.Create(_cookiesFilePath);
            _cookiesDictionary = new Dictionary<string, string>();
            await File.WriteAllTextAsync(_cookiesFilePath, JsonConvert.SerializeObject(_cookiesDictionary));
        }
    }

    private async Task SaveCookies()
    {

    }

    #endregion METHODS

}