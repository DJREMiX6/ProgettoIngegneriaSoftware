namespace ProgettoIngegneriaSoftware.UI.Exceptions.API;

public class BadRequestApiException : Exception
{
    public BadRequestApiException(string? message = null) : base(message ?? string.Empty) {}
}