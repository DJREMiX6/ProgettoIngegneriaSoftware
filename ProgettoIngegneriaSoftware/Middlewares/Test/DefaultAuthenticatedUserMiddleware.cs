using Microsoft.IdentityModel.Tokens;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Middlewares.Test
{
    public class DefaultAuthenticatedUserMiddleware
    {

        private readonly ILogger<DefaultAuthenticatedUserMiddleware> _logger;
        private readonly RequestDelegate _next;

        private UserModelRecord _userLoginInfo;

        public DefaultAuthenticatedUserMiddleware(ILogger<DefaultAuthenticatedUserMiddleware> logger, RequestDelegate next, UserModelRecord userLoginInfo)
        {
            _logger = logger;
            _next = next;
            _userLoginInfo = userLoginInfo;
        }

        public async Task Invoke(HttpContext context, IUserAuthenticationService userAuthenticationService, IUserModelService userModelService)
        {
            if (userAuthenticationService.IsUserAuthenticated(context))
            {
                await _next.Invoke(context);
                return;
            }

            await userModelService.CreateAsync(_userLoginInfo);
            var user = await userModelService.GetAsync(username: _userLoginInfo.Username);
            await userAuthenticationService.SignInAsync(context, user);
            await _next.Invoke(context);
        }

    }
}
