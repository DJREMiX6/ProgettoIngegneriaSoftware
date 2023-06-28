using ProgettoIngegneriaSoftware.API.Models;

namespace ProgettoIngegneriaSoftware.API.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        public bool IsUserAuthenticated(HttpContext httpContext);
        public Guid AuthenticatedUserId(HttpContext httpContext);
        public Task SignInAsync(HttpContext httpContext, LoginUserInfo loginUserInfo);
        public Task SignOutAsync(HttpContext httpContext);
    }
}
