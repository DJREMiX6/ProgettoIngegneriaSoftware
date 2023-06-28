using ProgettoIngegneriaSoftware.API.Models;

namespace ProgettoIngegneriaSoftware.API.Services.RegistrationService;

public interface IRegistrationService
{

    /// <summary>
    /// Creates a new user that can login.
    /// </summary>
    /// <param name="registerUserInfo">The registration information that is going to be used to create the user.</param>
    /// <exception cref="Exceptions.EmailAlreadyInUseException">Thrown when the email provided by the client is already in use from another user.</exception>
    /// <exception cref="Exceptions.UsernameAlreadyInUseException">Throw when the username provided by the client is already in use from another user</exception>
    /// <returns></returns>
    public Task RegisterUser(RegisterUserInfo registerUserInfo);

}