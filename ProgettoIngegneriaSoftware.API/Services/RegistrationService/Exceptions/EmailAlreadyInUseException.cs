namespace ProgettoIngegneriaSoftware.API.Services.RegistrationService.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    private const string EXCEPTION_MESSAGE = "Email already in use.";
    
    public EmailAlreadyInUseException() : base(EXCEPTION_MESSAGE){}
}