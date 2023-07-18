namespace ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

public interface IRegisterUserInfo
{

    #region PROPERTIES

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    #endregion PROPERTIES

}