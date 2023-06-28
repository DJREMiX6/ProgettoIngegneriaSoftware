using System.Security.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ProgettoIngegneriaSoftware.API.Models;
using System.Security.Claims;
using ProgettoIngegneriaSoftware.API.Services.UserEntityModelService;

namespace ProgettoIngegneriaSoftware.API.Services.AuthenticationService;

public class CookieBasedAuthenticationService : IAuthenticationService
{

    #region FIELDS

    private readonly ILogger<CookieBasedAuthenticationService> _logger;
    private readonly IUserEntityModelService _userEntityModelService;

    private readonly string _authenticationScheme;
    private const string ID_CLAIM_TYPE = "id";

    #endregion FIELDS

    #region CTORS

    public CookieBasedAuthenticationService(IUserEntityModelService userEntityModelService, ILogger<CookieBasedAuthenticationService> logger)
    {
        _userEntityModelService = userEntityModelService;
        _logger = logger;
        _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }

    #endregion CTORS

    #region IAuthenticationService IMPLEMENTATION

    public bool IsUserAuthenticated(HttpContext httpContext) => httpContext.User.FindFirst(ID_CLAIM_TYPE) != null;

    public Guid AuthenticatedUserId(HttpContext httpContext) => IsUserAuthenticated(httpContext) ? new Guid(httpContext.User.FindFirstValue(ID_CLAIM_TYPE)) : Guid.Empty;

    public async Task SignInAsync(HttpContext httpContext, LoginUserInfo loginUserInfo)
    {
        var userId = await _userEntityModelService.ExistsAsync(loginUserInfo.Email);

        if (userId == Guid.Empty)
            throw new AuthenticationException("Incorrect email.");
        if (!await _userEntityModelService.ValidateUserAsync(userId, loginUserInfo.Password))
            throw new AuthenticationException("Incorrect password.");

        var claims = new Claim[] { new Claim(ID_CLAIM_TYPE, userId.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims, _authenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContext.SignInAsync(_authenticationScheme, claimsPrincipal);
    }

    public async Task SignOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync();
    }

    #endregion IAuthenticationService IMPLEMENTATION

}