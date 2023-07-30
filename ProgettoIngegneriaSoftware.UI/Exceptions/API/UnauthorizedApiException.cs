namespace ProgettoIngegneriaSoftware.UI.Exceptions.API;

public class UnauthorizedApiException : Exception
{
    public UnauthorizedApiException(string? message = null) : base(message ?? string.Empty) {}
}