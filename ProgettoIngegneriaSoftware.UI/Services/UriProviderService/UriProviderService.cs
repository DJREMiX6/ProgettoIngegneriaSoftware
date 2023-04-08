using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.UI.Extensions;

namespace ProgettoIngegneriaSoftware.UI.Services.UriProviderService;

public class UriProviderService : IUriProviderService
{

    #region PRIVATE FIELDS

    private readonly IConfiguration _configuration;

    #endregion PRIVATE FIELDS

    #region CTORS

    public UriProviderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion CTORS

    #region IUriProviderService IMPLEMENTATION

    public Uri GetSignUpUri()
    {
        var endpointInfo = _configuration.GetLoginRegisterEndpoints().First(endpoint => endpoint.Name == "Register");
        var uriTokens = new string[]
        {
            _configuration.GetConnectionString("Debug")!,
            endpointInfo.ControllerName,
            endpointInfo.Endpoint
        };
        var uriString = string.Join("/", uriTokens);
        return new Uri(uriString);
    }

    #endregion IUriProviderService IMPLEMENTATION

}