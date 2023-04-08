using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;

namespace ProgettoIngegneriaSoftware.UI.Services.RegisterUserService;

public interface IRegisterUserService
{
    public Task<HttpResponseMessage> RegisterUser(UserModelRecord userModelRecord);
}