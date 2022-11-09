using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.Services;
using ProgettoIngegneriaSoftware.Utils.Extensions;
using ProgettoIngegneriaSoftware.Utils.Consts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterController : Controller
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterController> _logger;
        private readonly IUserModelService _userModelService;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterController(ILogger<LoginRegisterController> logger, IUserModelService userModelService)
        {
            _logger = logger;
            _userModelService = userModelService;
        }

        #endregion CTORS

        #region HTTP ACTIONS

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("signup", Name = "SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserModelRecord userModelRecord)
        {
            //Checks if a user is already logged in
            var userAlreadyLoggedInUsername = HttpContext.User.FindFirst("username")?.Value;
            if (userAlreadyLoggedInUsername is not null)
            {
                return Unauthorized("User already logged in.");
            }

            //Checks if the email is already in use
            var emailIsInUse = await _userModelService.ExistsAsync(email: userModelRecord.Email);
            if (emailIsInUse)
            {
                return BadRequest("Email already in use.");
            }

            //Checks if the username is already in use
            var usernameIsInUse = await _userModelService.ExistsAsync(username: userModelRecord.Username);
            if (usernameIsInUse)
            {
                return BadRequest("Username already in use.");
            }

            //Creates the user and adds it to the storage
            var user = await _userModelService.CreateAsync(userModelRecord);
            if (user is null)
            {
                return BadRequest("Error while signing up.");
            }

            return Ok(new { Message = "User registered.", ConfirmationToken = user.ConfirmationToken });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("signin", Name = "SignIn")]
        public async Task<IActionResult> SignIn([FromHeader] string username, [FromHeader] string password)
        {
            //Checks if a user is already logged in
            var userAlreadyLoggedInUsername = HttpContext.User.FindFirst("username")?.Value;
            if (userAlreadyLoggedInUsername is not null)
            {
                return Unauthorized("User already logged in.");
            }

            //Checks if a user with the given username exists
            var user = await _userModelService.GetAsync(username: username);
            if (user is null)
            {
                return NotFound("User not found");
            }

            //Checks if the password is valid
            if (!await _userModelService.ValidatePasswordAsync(user, password))
            {
                return BadRequest("Password is incorrect.");
            }

            //Checks if the user has been verified
            if (!user.IsConfirmed)
            {
                return BadRequest("User is not confirmed.");
            }

            //Creates necessary claims to sign in the user
            var claims = new List<Claim> { new Claim("username", username) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok("User logged in");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("signout", Name = "SignOut")]
        public new async Task<IActionResult> SignOut()
        {
            //Checks if a user is already logged in
            var userAlreadyLoggedInUsername = HttpContext.User.FindFirst("username")?.Value;
            if (userAlreadyLoggedInUsername is null)
            {
                return Unauthorized();
            }

            //Signs our the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("User signed out correctly.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("confirm-user/{confirmationToken}")]
        public async Task<IActionResult> ConfirmUser(long confirmationToken)
        {
            //Checks if a user with the given confirmationToken exists
            var user = await _userModelService.GetAsync(confirmationToken);
            if (user is null)
            {
                return NotFound("No user to confirm has been found.");
            }

            //Confirms the user
            await _userModelService.ConfirmAsync(confirmationToken);
            return Ok("User confirmed.");
        }

        #endregion HTTP ACTIONS

    }
}
