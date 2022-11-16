using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<UserController> _logger;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IUserModelService _userModelService;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public UserController(ILogger<UserController> logger, IUserAuthenticationService userAuthenticationService, IUserModelService userModelService)
        {
            _logger = logger;
            _userAuthenticationService = userAuthenticationService;
            _userModelService = userModelService;
        }

        #endregion CTORS

        #region HTTP ACTIONS

        #region READ

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("", Name = "GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (!IsUserAuthenticated())
            {
                return Unauthorized("User not signed in.");
            }

            var user = await _userModelService.GetAsync(AuthenticatedUserId());
            if (user is null)
            {
                await _userAuthenticationService.SignOutAsync(HttpContext);
                return Unauthorized("The user does not exists.");
            }

            return Ok(user);
        }

        #endregion READ

        #region UPDATE



        #endregion UPDATE

        #region DELETE



        #endregion DELETE

        #endregion HTTP ACTIONS

        #region PRIVATE METHODS

        [NonAction]
        private bool IsUserAuthenticated() => _userAuthenticationService.IsUserAuthenticated(HttpContext);

        [NonAction]
        private Guid AuthenticatedUserId() => _userAuthenticationService.AuthenticatedUserId(HttpContext) ?? throw new NullReferenceException("Logged in user Id is null.");

        #endregion PRIVATE METHODS

    }
}
