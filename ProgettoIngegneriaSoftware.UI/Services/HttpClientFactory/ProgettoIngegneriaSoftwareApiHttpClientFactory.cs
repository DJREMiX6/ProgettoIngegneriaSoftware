using ProgettoIngegneriaSoftware.UI.Services.UriService;

namespace ProgettoIngegneriaSoftware.UI.Services.HttpClientFactory;

public class ProgettoIngegneriaSoftwareApiHttpClientFactory
{

    #region FIELDS

    private readonly IServiceProvider _serviceProvider;

    #endregion FIELDS

    #region CTORS

    public ProgettoIngegneriaSoftwareApiHttpClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion CTORS

    public ProgettoIngegneriaSoftwareApiHttpClient Make()
    {
        var uriServiceDependency = ActivatorUtilities.CreateInstance<IUriService>(_serviceProvider);
        return new ProgettoIngegneriaSoftwareApiHttpClient(uriServiceDependency);
    }
}