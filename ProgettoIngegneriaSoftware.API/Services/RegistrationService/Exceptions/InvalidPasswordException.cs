namespace ProgettoIngegneriaSoftware.API.Services.RegistrationService.Exceptions
{
    public class InvalidPasswordException : Exception
    {

        private const string EXCEPTION_MESSAGE = "Invalid password.";

        public InvalidPasswordException() : base(EXCEPTION_MESSAGE) { }

    }
}
