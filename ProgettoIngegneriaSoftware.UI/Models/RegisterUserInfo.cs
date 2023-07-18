using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Models;

public class RegisterUserInfo : IRegisterUserInfo
{

    #region PROPS

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    #endregion PROPS

}