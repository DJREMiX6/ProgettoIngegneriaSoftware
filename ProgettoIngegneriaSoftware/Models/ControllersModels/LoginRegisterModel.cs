using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.Tokenization;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Utils.Extensions;

namespace ProgettoIngegneriaSoftware.Models.ControllersModels
{
    public class LoginRegisterModel : ILoginRegisterModel
    {

        #region PRIVATE CONSTS

        private const int MINIMUM_PASSWORD_LENGTH = 8;

        #endregion PRIVATE CONSTS

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<LoginRegisterModel> _logger;
        private readonly AutenticationDbContext _autenticationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginRegisterModel(
            ILogger<LoginRegisterModel> logger, 
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

        public bool IsPasswordValid(string password, string confirmPassword)
        {
            if(password.Length < MINIMUM_PASSWORD_LENGTH)
            {
                return false;
            }
            if(password.Where(c => char.IsSymbol(c) || char.IsPunctuation(c)).Count() == 0)
            {

            }
            if(password.Where(c => char.IsDigit(c)).Count() == 0)
            {
                return false;
            }
            if(password.Where(c => char.IsUpper(c)).Count() == 0)
            {
                return false;
            }
            if(password.Where(c => char.IsLower(c)).Count() == 0)
            {
                return false;
            }
            if(!IsPasswordEqualToConfirmPassword(password, confirmPassword))
            {
                return false;
            }

            return true;
        }
        public async Task<bool> IsPasswordValid(string password, UserModel user)
        {
            return await _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt, true);
        }

        public async Task<bool> IsLoginTokenValid(string loginToken, string username)
        {
            if(loginToken.Trim().Equals(string.Empty))
            {
                return false;
            }
            var queryToken = await _autenticationDbContext.LoginTokens.FirstOrDefaultAsync(token => token.Token.Equals(loginToken));
            if(queryToken is null)
            {
                return false;
            }
            var queryUser = await GetUserByUsername(username);
            if(queryUser is null)
            {
                return false;
            }
            if(queryToken.User != queryUser)
            {
                return false;
            }
            if(((TimeSpan)(queryToken.ExpirationDate - DateTime.Now)).Milliseconds <= 0)
            {
                queryToken.IsExpired = true;
                await _autenticationDbContext.SaveChangesAsync();
            }
            if(queryToken.IsExpired)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsLoginTokenValid(string loginToken, UserModel user)
        {
            if (loginToken.Trim().Equals(string.Empty))
            {
                return false;
            }
            var queryToken = await _autenticationDbContext.LoginTokens.FirstOrDefaultAsync(token => token.Token.Equals(loginToken));
            if (queryToken is null)
            {
                return false;
            }
            if (queryToken.User != user)
            {
                return false;
            }
            if (((TimeSpan)(queryToken.ExpirationDate - DateTime.Now)).Milliseconds <= 0)
            {
                queryToken.IsExpired = true;
                await _autenticationDbContext.SaveChangesAsync();
            }
            if (queryToken.IsExpired)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> LoginTokenExists(string loginToken)
        {
            return await _autenticationDbContext.LoginTokens.FirstOrDefaultAsync(token => token.Token.Equals(loginToken)) != null;
        }

        public async Task<bool> UserExistsByUsername(string username)
        {
            
            return await GetUserByUsername(username) != null;
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await GetUserByEmail(email) != null;
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

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(username));
        }

        public async Task<UserModel?> GetUserByEmail(string email)
        {
            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public async Task<string> GenerateToken(UserModel user)
        {
            var loginToken = String.Empty;
            do
            {
                loginToken = _tokenGenerator.GenerateToken(LoginTokenModel.TOKEN_LENGTH);
            } while (await LoginTokenExists(loginToken));

            await _autenticationDbContext.LoginTokens.AddAsync(new LoginTokenModel()
            {
                Token = loginToken,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.MaxValue,
                IsExpired = false,
                User = user
            });
            await _autenticationDbContext.SaveChangesAsync();

            return loginToken;
        }

        public async Task<bool> ExpireLoginToken(string loginToken)
        {
            var token = await _autenticationDbContext.LoginTokens.FirstOrDefaultAsync(queryToken => queryToken.Token.Equals(loginToken));
            if(token == null)
            {
                return false;
            }
            token.IsExpired = true;
            await _autenticationDbContext.SaveChangesAsync();
            return true;
        }

        #endregion PUBLIC METHODS

        #region PRIVATE METHODS

        private bool IsPasswordEqualToConfirmPassword(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }

        #endregion PRIVATE METHODS

    }
}
