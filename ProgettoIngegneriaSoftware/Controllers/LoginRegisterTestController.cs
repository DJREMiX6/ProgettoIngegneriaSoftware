using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Security;

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

        #endregion PRIVATE READONLY DI FIELDS

        public LoginRegisterTestController(
            AutenticationDbContext autenticationDbContext, 
            ILogger<LoginRegisterTestController> logger,
            IPasswordHasher passwordHasher
            )
        {
            _autenticationDbContext = autenticationDbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

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
            var hashResult = _passwordHasher.HashPassword(password, null, true);
            var newUser = new UserModel()
            {
                UserName = username,
                StoredPassword = hashResult.Hash,
                StoredSalt = hashResult.Salt,
                Email = email
            };
            _logger.LogInformation("New User = {newUser}", newUser);
            await _autenticationDbContext.Users.AddAsync(newUser);
            await _autenticationDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
