using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.ControllersModels;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.HttpResponseObjects;
using ProgettoIngegneriaSoftware.Models.Tokenization;
using ProgettoIngegneriaSoftware.Security;
using System.Text;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterTestController : Controller
    {

        #region PRIVATE CONSTS

        private const string COOKIE_LOGIN_TOKEN_NAME = "LoginToken";

        #endregion PRIVATE CONSTS

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterTestController> _logger;
        private readonly LoginRegisterTestModel _model;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterTestController(ILogger<LoginRegisterTestController> logger, LoginRegisterTestModel model)
        {
            _logger = logger;
            _model = model;
        }

        #endregion CTORS

        #region HTTP CALLS

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromHeader]string email, [FromHeader]string username, [FromHeader]string password, [FromHeader]string confirmPassword)
        {
            //ERRORS HANDLING
            if(!_model.IsPasswordValid(password))
            {
                return BadRequest(Json(new BadRequestData("Invalid password.")));
            }
            if(!_model.IsPasswordEqualToConfirmPassword(password, confirmPassword))
            {
                return BadRequest(Json(new BadRequestData("Fields password and confirmPassword are not equal.")));
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
            _model.CreateNewUser(username, email, password);
            _logger.LogInformation("New User created.");
            await _autenticationDbContext.Users.AddAsync(newUser);
            await _autenticationDbContext.SaveChangesAsync();
            return Ok(Json(new OkData("User registered.")));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromHeader]string username, [FromHeader]string password)
        {
            //ERRORS HANDLING
            var testUser = _autenticationDbContext.Users.FirstOrDefault(user => user.UserName.Equals(username));
            if(testUser is null)
            {
                _logger.LogInformation("User not found.");
                return BadRequest(Json(new { error = "No user found with this username." }));
            }
            if(await _passwordHasher.VerifyPassword(password, testUser.PasswordHash, testUser.Salt, true) == false)
            {
                _logger.LogInformation("User password is incorrect.");
                return BadRequest(Json(new { error = "Password is incorrect." }));
            }
            var userLoginTokenValue = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];
            if(userLoginTokenValue != null && !userLoginTokenValue.Equals(string.Empty))
            {
                _logger.LogInformation("LoginToken found on request cookies: {userLoginTokenValue}", userLoginTokenValue);
                var userLoginToken = _autenticationDbContext.LoginTokens.FirstOrDefault(token => token.User == testUser && token.Token.Equals(userLoginTokenValue));
                if(userLoginToken != null && !userLoginToken.IsExpired)
                {
                    _logger.LogInformation("LoginToken found and it's not expired, user already autenticated.");
                    return BadRequest(Json(new { error = "User already autenticated." }));
                }
            }
            var isPasswordValid = await _passwordHasher.VerifyPassword(password, testUser.PasswordHash, testUser.Salt, true);
            if(!isPasswordValid)
            {
                _logger.LogInformation("Invalid user password.");
                return BadRequest(Json(new { error = "Invalid user password." }));
            }

            //TOKEN GENERATION
            var loginToken = String.Empty;
            do
            {
                _logger.LogInformation("Generating LoginToken...");
                loginToken = _tokenGenerator.GenerateToken(LoginTokenModel.TOKEN_LENGTH);
                _logger.LogInformation("LoginToken generated: {loginToken}", loginToken);
            } while (_autenticationDbContext.LoginTokens.FirstOrDefault(queryToken => queryToken.Token.Equals(loginToken)) != null);
            await _autenticationDbContext.LoginTokens.AddAsync(new LoginTokenModel() {
                Token = loginToken,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.MaxValue,
                IsExpired = false,
                User = testUser
                });
            await _autenticationDbContext.SaveChangesAsync();

            Response.Cookies.Append(COOKIE_LOGIN_TOKEN_NAME, loginToken);
            return Ok(Json(new { message = "User autenticated.", loginToken = loginToken }));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromHeader]string username)
        {
            //ERRORS HANDLING
            string? loginTokenValue = Request.Cookies[COOKIE_LOGIN_TOKEN_NAME];
            if(loginTokenValue == null)
            {
                _logger.LogInformation("Cookie LoginToken is null");
                return BadRequest(Json(new { error = "Login token is null." }));
            }
            LoginTokenModel? loginToken = await _autenticationDbContext.LoginTokens.FirstOrDefaultAsync(loginToken => loginToken.Token.Equals(loginTokenValue) && loginToken.User.UserName.Equals(username));
            if(loginToken == null)
            {
                _logger.LogInformation("LoginToken is invalid.");
                return BadRequest(Json(new { error = "Login token is invalid." }));
            }

            Response.Cookies.Delete(COOKIE_LOGIN_TOKEN_NAME);
            loginToken.IsExpired = true;
            await _autenticationDbContext.SaveChangesAsync();
            _logger.LogInformation("LoginToken expired and user logged out.");

            return Ok(Json(new { message = "User logged out correctly." }));
        }

        #endregion HTTP CALLS

    }
}
