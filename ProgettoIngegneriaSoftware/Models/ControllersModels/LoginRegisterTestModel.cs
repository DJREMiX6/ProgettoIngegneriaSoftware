using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.Tokenization;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Utils.Extensions;

namespace ProgettoIngegneriaSoftware.Models.ControllersModels
{
    public class LoginRegisterTestModel
    {

        #region PRIVATE CONSTS

        private const int MINIMUM_PASSWORD_LENGTH = 8;
        public static readonly char[] SPECIAL_CHARACTERS = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', '-', '/', '@', ':', ';', '<', '>', '?', '@', '\\', '[', ']', '^', '_' };

        #endregion PRIVATE CONSTS

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterTestModel> _logger;
        private readonly AutenticationDbContext _autenticationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterTestModel(
            ILogger<LoginRegisterTestModel> logger, 
            AutenticationDbContext autenticationDbContext,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator
            )
        {
            _logger = logger;
            _autenticationDbContext = autenticationDbContext;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        #endregion CTORS

        #region PUBLIC METHODS

        public bool IsPasswordValid(string password)
        {
            if(password.Length < MINIMUM_PASSWORD_LENGTH)
            {
                return false;
            }
            if(!password.Contains(SPECIAL_CHARACTERS))
            {
                return false;
            }

            return true;
        }

        public bool IsPasswordEqualToConfirmPassword(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }

        public async Task<bool> UserExistsByUsername(string username)
        {
            
            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(username)) != null;
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email)) != null;
        }

        public async Task<bool> CreateNewUser(string username, string email, string password)
        {
            try
            {
                var hashResult = await _passwordHasher.HashPassword(password);
                var user = new UserModel()
                {
                    UserName = username,
                    Email = email,
                    PasswordHash = hashResult.Hash,
                    Salt = hashResult.Salt
                };

                await _autenticationDbContext.Users.AddAsync(user);
                await _autenticationDbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error during the creation of a new user.");
                return false;
            }
        }

        #endregion PUBLIC METHODS

    }
}
