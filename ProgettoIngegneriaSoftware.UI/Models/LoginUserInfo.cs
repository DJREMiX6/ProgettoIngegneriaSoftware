using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Models;

public class LoginUserInfo : ILoginUserInfo
{

    #region PROPS

    public string Email { get; set; }
    public string Password { get; set; }

    #endregion PROPS

}