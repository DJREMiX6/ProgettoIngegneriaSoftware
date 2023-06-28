namespace ProgettoIngegneriaSoftware.API.Services.RegistrationService.Exceptions;

public class UsernameAlreadyInUseException : Exception
{
    private const string EXCEPTION_MESSAGE = "Username already in use.";

    public UsernameAlreadyInUseException() : base(EXCEPTION_MESSAGE) { }
}