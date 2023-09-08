namespace ProgettoIngegneriaSoftware.UI.Services.UriService;

public class UriService : IUriService
{

    #region CONSTS

    //TODO MOVE TO APPSETTINGS.JS

    private const string SCHEME = "http";
    private const string HOSTNAME = "192.168.1.5";
    private const string PORT = "8089";
    private const string API_PATH = "api";
    private const string BASE_AUTHENTICATION_PATH = "Authentication";
    private const string AUTHENTICATION_SIGN_IN_PATH = "signin";
    private const string AUTHENTICATION_SIGN_UP_PATH = "signup";
    private const string AUTHENTICATION_SIGN_OUT_PATH = "signout";
    private const string BASE_EVENTS_PATH = "Events";

    #endregion CONSTS

    #region IUriService IMP

    public string BasePath() => $"{SCHEME}://{HOSTNAME}:{PORT}"; 

    public string BaseApiPath() => $"{BasePath()}/{API_PATH}";

    #region AUTHENTICATION

    private string BaseAuthenticationPath() => $"{BaseApiPath()}/{BASE_AUTHENTICATION_PATH}";

    public string AuthenticationSignInPath() => $"{BaseAuthenticationPath()}/{AUTHENTICATION_SIGN_IN_PATH}";

    public string AuthenticationSignUpPath() => $"{BaseAuthenticationPath()}/{AUTHENTICATION_SIGN_UP_PATH}";

    public string AuthenticationSignOutPath() => $"{BaseAuthenticationPath()}/{AUTHENTICATION_SIGN_OUT_PATH}";

    #endregion AUTHENTICATION

    #region EVENTS

    private string BaseEventsPath() => $"{BaseApiPath()}/{BASE_EVENTS_PATH}";

    public string AllEventsPath() => $"{BaseEventsPath()}/";

    public string EventPath(Guid eventId) => $"{BaseEventsPath()}/{eventId}";

    #endregion EVENTS

    #endregion IUriService IMP

}