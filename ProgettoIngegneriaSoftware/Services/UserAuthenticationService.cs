using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {

        public readonly string AuthenticationScheme;
        private readonly string[] _claimTypes;

        private const string USERNAME_CLAIM_TYPE = "username";
        private const string ID_CLAIM_TYPE = "id";


        public UserAuthenticationService()
        {
            AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            _claimTypes = new[]
            {
                USERNAME_CLAIM_TYPE,
                ID_CLAIM_TYPE
            };
        }

        public bool IsUserAuthenticated(HttpContext httpContext)
        {
            foreach (var claimType in _claimTypes)
            {
                var claim = httpContext.User.FindFirst(claimType);
                if (claim is null)
                {
                    return false;
                }
            }

            return true;
        }

        public Guid? AuthenticatedUserId(HttpContext httpContext)
        {
            if (!IsUserAuthenticated(httpContext))
            {
                return null;
            }

            return new Guid(httpContext.User.FindFirstValue(ID_CLAIM_TYPE));
        }

        public string? AuthenticatedUserUsername(HttpContext httpContext)
        {
            if (!IsUserAuthenticated(httpContext))
            {
                return null;
            }

            return httpContext.User.FindFirstValue(USERNAME_CLAIM_TYPE);
        }


        public async Task SignInAsync(HttpContext httpContext, UserModel userModel)
        {
            var claims = new Claim[]
            {
                new Claim(USERNAME_CLAIM_TYPE, userModel.Username),
                new Claim(ID_CLAIM_TYPE, userModel.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(AuthenticationScheme, claimsPrincipal);
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
