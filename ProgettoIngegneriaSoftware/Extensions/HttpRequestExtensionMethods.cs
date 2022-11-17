namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class HttpRequestExtensionMethods
    {
        public static string GetBaseUrl(this HttpRequest request) => string.Format("{0}://{1}{2}/", request.Scheme,
            request.Host.Host, request.Host.Port is null ? string.Empty : ":" + request.Host.Port);
    }
}
