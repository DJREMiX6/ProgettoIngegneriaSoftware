using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.UI.Services.UriProviderService;

namespace ProgettoIngegneriaSoftware.UI.Services.RegisterUserService;

public class RegisterUserService : IRegisterUserService
{

    #region PRIVATE FIELDS

    private readonly IConfiguration _configuration;
    private readonly IUriProviderService _uriProviderService;

    #endregion PRIVATE FIELDS

    #region CTORS

    public RegisterUserService(IConfiguration configuration, IUriProviderService uriProviderService)
    {
        _configuration = configuration;
        _uriProviderService = uriProviderService;
    }

    #endregion CTORS

    #region IRegisterUserService IMPLEMENTATION

    public async Task<HttpResponseMessage> RegisterUser(UserModelRecord userModelRecord)
    {
        var httpClient = new HttpClient();
        var signupUri = _uriProviderService.GetSignUpUri();
        var responseMessage = await httpClient.PostAsJsonAsync(signupUri, userModelRecord);
        return responseMessage;
    }

    #endregion IRegisterUserService IMPLEMENTATION

}