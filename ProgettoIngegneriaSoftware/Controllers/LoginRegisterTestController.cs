using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.Tokenization;
using ProgettoIngegneriaSoftware.Security;
using System.Text;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterTestController : Controller
    {
        #region PRIVATE READONLY DI FIELDS

        private readonly AutenticationDbContext _autenticationDbContext;
        private readonly ILogger<LoginRegisterTestController> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterTestController(
            AutenticationDbContext autenticationDbContext, 
            ILogger<LoginRegisterTestController> logger,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator
            )
        {
            _autenticationDbContext = autenticationDbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        #endregion CTORS

        #region HTTP CALLS

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromHeader]string email, [FromHeader]string username, [FromHeader]string password, [FromHeader]string confirmPassword)
        {
            //ERRORS HANDLING
            if(!string.Equals(password, confirmPassword))
            {
                return BadRequest(Json(new { error = "Fields password and confirmPassword are not equal." }));
            }
            if (await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(username)) != null)
            {
                return BadRequest(Json(new { error = "Username already in use by another user." }));
            }
            if(await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email)) != null)
            {
                return BadRequest(Json(new { error = "Email already in use by another user." }));
            }

            //New user creation and insertion into the database
            HashResult hashResult = await _passwordHasher.HashPassword(password, null, true);
            var newUser = new UserModel()
            {
                UserName = username,
                PasswordHash = hashResult.Hash,
                Salt = hashResult.Salt,
                Email = email
            };
            _logger.LogInformation("New User = {newUser}", newUser);
            await _autenticationDbContext.Users.AddAsync(newUser);
            await _autenticationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromHeader]string email, [FromHeader]string password)
        {
            //ERRORS HANDLING
            var testUser = _autenticationDbContext.Users.FirstOrDefault(user => user.Email.Equals(email));
            if(testUser is null)
            {
                return BadRequest(Json(new { error = "No user found with this email." }));
            }
            var userLoginTokenValue = Request.Cookies["LoginToken"];
            var userLoginToken = _autenticationDbContext.LoginTokens.FirstOrDefault(token => token.Token.Equals(userLoginTokenValue));
            if (
                userLoginToken != null 
                && !userLoginToken.Equals(string.Empty) 
                && userLoginToken != null 
                && !userLoginToken.IsExpired
            )
            {
                return BadRequest(Json(new { error = "User already autenticated." }));
            }
            var isPasswordValid = await _passwordHasher.VerifyPassword(password, testUser.PasswordHash, testUser.Salt, true);
            if(!isPasswordValid)
            {
                return BadRequest(Json(new { error = "Invalid user password." }));
            }

            //TOKEN GENERATION
            var loginToken = String.Empty;
            do
            {
                _logger.LogInformation("Generating LoginToken...");
                loginToken = _tokenGenerator.GenerateToken(LoginTokenModel.TOKEN_LENGTH);
                _logger.LogInformation("LoginToken generated = {loginToken}", loginToken);
            } while (_autenticationDbContext.LoginTokens.FirstOrDefault(queryToken => queryToken.Equals(loginToken)) != null);

            Response.Cookies.Append("LoginToken", loginToken);
            return Json(new { message = "User autenticated.", loginToken = loginToken });
        }

        #endregion HTTP CALLS

    }
}
