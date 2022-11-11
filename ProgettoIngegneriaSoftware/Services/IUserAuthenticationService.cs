using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Services
{
    public interface IUserAuthenticationService
    {
        public bool IsUserAuthenticated(HttpContext httpContext);
        public Guid? AuthenticatedUserId(HttpContext httpContext);
        public string? AuthenticatedUserUsername(HttpContext httpContext);
        public Task SignInAsync(HttpContext httpContext, UserModel userModel);
        public Task SignOutAsync(HttpContext httpContext);
    }
}
