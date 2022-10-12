using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.ControllersModels;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.HttpResponseObjects;
using ProgettoIngegneriaSoftware.Models.Tokenization;
using ProgettoIngegneriaSoftware.Security;
using System.Net;
using System.Text;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterController : Controller
    {

        #region PRIVATE CONSTS

        private const string COOKIE_LOGIN_TOKEN_NAME = "LoginToken";

        #endregion PRIVATE CONSTS

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterController> _logger;
        private readonly ILoginRegisterModel _model;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterController(ILogger<LoginRegisterController> logger, LoginRegisterModel model)
        {
            _logger = logger;
            _model = model;
        }

        #endregion CTORS

        #region HTTP ACTIONS

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromHeader]string email, [FromHeader]string username, [FromHeader]string password, [FromHeader]string confirmPassword)
        {
            //ERRORS HANDLING
            if(!_model.IsPasswordValid(password, confirmPassword))
            {
                return BadRequest(Json(new BadRequestData("Invalid passwords.")));
            }
            if (await _model.UserExistsByUsername(username))
            {
                return BadRequest(Json(new BadRequestData("Username already in use by another user.")));
            }
            if(await _model.UserExistsByEmail(email))
            {
                return BadRequest(Json(new BadRequestData("Email already in use by another user.")));
            }

            //New user creation and insertion into the database
            if(!await _model.CreateNewUser(username, email, password))
            {
                _logger.LogError("Error while creating the user.");
                throw new Exception("Error while registering the user.");
            }

            _logger.LogInformation("User registered correctly.");
            return Ok(Json(new OkData("User registered.")));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromHeader] string username, [FromHeader] string password)
        {
            //ERRORS HANDLING
            var user = await _model.GetUserByUsername(username);
            var userLoginTokenValue = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];

            if (user is null)
            {
                _logger.LogInformation("User not found.");
                return BadRequest(Json(new BadRequestData("No user found with this username.")));
            }
            if (await _model.IsPasswordValid(password, user))
            {
                _logger.LogInformation("User password is incorrect.");
                return BadRequest(Json(new BadRequestData("Password is incorrect.")));
            }
            if(userLoginTokenValue != null)
            {
                if (!await _model.IsLoginTokenValid(userLoginTokenValue, user))
                {
                    Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
                    return BadRequest(Json(new BadRequestData("Invalid LoginToken.")));
                }
                else
                {
                    return BadRequest(Json(new BadRequestData("User already logged.")));
                }
            }

            //TOKEN GENERATION
            string token = await _model.GenerateToken(user);

            Response.Cookies.Append(COOKIE_LOGIN_TOKEN_NAME, token);
            return Ok(Json(new OkData("User autenticated")));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromHeader] string username)
        {
            //ERRORS HANDLING
            var loginTokenValue = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];
            if (loginTokenValue == null)
            {
                _logger.LogInformation("Cookie LoginToken is null");
                return BadRequest(Json(new BadRequestData("Login token not found.")));
            }
            var user = await _model.GetUserByUsername(username);
            if(user is null)
            {
                _logger.LogInformation("No user found.");
                return BadRequest(Json(new BadRequestData("No user found.")));
            }
            if (!await _model.IsLoginTokenValid(loginTokenValue, user))
            {
                _logger.LogInformation("LoginToken is not valid.");
                return BadRequest(Json(new BadRequestData("Login token is not valid.")));
            }

            Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
            await _model.ExpireLoginToken(loginTokenValue);
            _logger.LogInformation("LoginToken expired and user logged out.");

            return Ok(Json(new OkData("User logged out correctly")));
        }

        #endregion HTTP ACTIONS

        #region ACTION RESULTS

        [NonAction]
        public InternalServerErrorObjectResult InternalServerError([ActionResultObjectValue] object? error) 
            => new InternalServerErrorObjectResult(error);

        [NonAction]
        public InternalServerErrorObjectResult InternalServerError([ActionResultObjectValue] ModelStateDictionary modelState)
            => new InternalServerErrorObjectResult(modelState);

        #endregion ACTION RESULTS

    }
}
