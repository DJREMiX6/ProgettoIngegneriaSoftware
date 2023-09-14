namespace ProgettoIngegneriaSoftware.UI.Exceptions.API;

public class NotFoundApiException : Exception
{
    public NotFoundApiException(string? message = null) : base(message ?? string.Empty) {}
}