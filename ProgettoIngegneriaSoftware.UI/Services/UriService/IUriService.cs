namespace ProgettoIngegneriaSoftware.UI.Services.UriService;

public interface IUriService
{

    public string BasePath();
    public string BaseApiPath();

    #region AUTHENTICATION

    public string AuthenticationSignInPath(); 
    public string AuthenticationSignUpPath();
    public string AuthenticationSignOutPath();

    #endregion AUTHENTICATION

    #region EVENTS

    public string AllEventsPath();
    public string EventPath(Guid eventId);

    #endregion EVENTS

    #region BOOKING

    public string BookSeatPath(Guid eventId);

    public string CancelBookedSeatPath(Guid eventId);

    #endregion BOOKING

}