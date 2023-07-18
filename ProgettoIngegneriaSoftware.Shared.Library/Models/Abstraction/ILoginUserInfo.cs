namespace ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

public interface ILoginUserInfo
{
    #region PROPERTIES

    public string Email { get; set; }
        
    public string Password { get; set; }

    #endregion PROPERTIES
}