using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.Services;
using ProgettoIngegneriaSoftware.Utils.Extensions;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterController : Controller
    {

        #region PRIVATE CONSTS

        private const string COOKIE_LOGIN_TOKEN_NAME = "LoginToken";

        #endregion PRIVATE CONSTS

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterController> _logger;
        private readonly IUserModelService _userModelService;
        private readonly ILoginTokenModelService _loginTokenModelService;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterController(ILogger<LoginRegisterController> logger, IUserModelService userModelService, ILoginTokenModelService loginTokenModelService)
        {
            _logger = logger;
            _userModelService = userModelService;
            _loginTokenModelService = loginTokenModelService;
        }

        #endregion CTORS

        #region HTTP ACTIONS

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/register", Name = "RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromBody]UserModelRecord userToRegister)
        {
            //If username is already in use
            if (await _userModelService.ExistsAsync(username: userToRegister.Username))
            {
                return BadRequest("Username already in use.");
            }

            //If email is already in use
            if (await _userModelService.ExistsAsync(email: userToRegister.Email))
            {
                return BadRequest("Email already in use.");
            }

            //Register the user
            var registeredUser = await _userModelService.CreateAsync(userToRegister);

            if (registeredUser is null)
            {
                return BadRequest("Error while registering.");
            }

            return Ok(new { Message = "User registered.", ConfirmationToken = registeredUser.ConfirmationToken });
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/login", Name = "LoginUser")]
        public async Task<IActionResult> LoginUser([FromHeader]string username, [FromHeader]string password)
        {
            var user = await _userModelService.GetAsync(username: username);

            if (user is null)
            {
                return NotFound("User not found.");
            }

            if (!await _userModelService.ValidateAsync(username, password))
            {
                return BadRequest("Password is incorrect.");
            }

            if (!user.IsConfirmed)
            {
                return BadRequest("User not confirmed.");
            }

            var requestLoginToken = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];
            if (requestLoginToken is not null)
            {
                await _loginTokenModelService.ExpireAsync(requestLoginToken);
                Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
            }

            var loginToken = await _loginTokenModelService.CreateAsync(user);
            if (loginToken is null)
            {
                return BadRequest("Error while logging in.");
            }

            Response.Cookies.Append(COOKIE_LOGIN_TOKEN_NAME, loginToken.Token);
            return Ok("User logged in correctly.");
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/logout", Name = "LogoutUser")]
        public async Task<IActionResult> LogoutUser([FromHeader] string username)
        {
            var user = await _userModelService.GetAsync(username);
            if (user is null)
            {
                Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
                return NotFound("User not found.");
            }

            var requestLoginToken = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];
            if (requestLoginToken is null)
            {
                return BadRequest("Login token not provided.");
            }

            if (!await _loginTokenModelService.IsValidAsync(username, requestLoginToken))
            {
                Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
                return BadRequest("No valid login token provided.");
            }

            var expiredLoginToken = await _loginTokenModelService.ExpireAsync(requestLoginToken);
            if (expiredLoginToken is null)
            {
                return BadRequest("Error while logging out the user.");
            }

            Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
            return Ok("User logged out correctly.");
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("/confirm-user/{confirmationToken:long}", Name = "ConfirmUser")]
        public async Task<IActionResult> ConfirmUser([FromRoute(Name = "confirmationToken")]long confirmationToken)
        {
            if (!confirmationToken.IsValidConfirmationToken())
            {
                return BadRequest("Invalid confirmation token.");
            }

            var user = await _userModelService.GetAsync(confirmationToken);
            if (user is null)
            {
                return NotFound("User not found.");
            }

            var confirmedUser = await _userModelService.ConfirmAsync(confirmationToken);
            if (confirmedUser is null)
            {
                return BadRequest("Error while confirming the user.");
            }

            return Ok("User confirmed.");
        }

        #endregion HTTP ACTIONS

    }
}
