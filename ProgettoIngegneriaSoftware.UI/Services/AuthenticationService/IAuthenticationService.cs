using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Services.AuthenticationService;

public interface IAuthenticationService
{
    public bool IsUserAuthenticated();
    public Task SignInAsync(ILoginUserInfo loginUserInfo);
}