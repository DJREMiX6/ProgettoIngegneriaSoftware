using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Services
{
    public class LoginTokenModelService : ILoginTokenModelService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly ILogger<LoginTokenModelService> _logger;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IUserModelService _userModelService;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public LoginTokenModelService(AuthenticationDbContext authenticationDbContext,
            ILogger<LoginTokenModelService> logger,
            ITokenGeneratorService tokenGeneratorService, 
            IUserModelService userModelService)
        {
            _authenticationDbContext = authenticationDbContext;
            _logger = logger;
            _tokenGeneratorService = tokenGeneratorService;
            _userModelService = userModelService;
        }

        #endregion CTORS
        
        #region CREATE

        public async Task<LoginTokenModel?> CreateAsync(UserModel userModel)
        {
            var user = await _userModelService.GetAsync(username: userModel.Username);
            if (user is null)
            {
                return null;
            }

            var loginTokenValue = await GenerateTokenAsync();
            var loginTokenModel = new LoginTokenModel()
            {
                Token = loginTokenValue,
                UserId = user.Id,
                User = user,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(1)
            };

            var returningLoginTokenEntity = await _authenticationDbContext.LoginTokens.AddAsync(loginTokenModel);
            await _authenticationDbContext.SaveChangesAsync();

            return returningLoginTokenEntity.Entity;
        }

        public async Task<LoginTokenModel?> CreateAsync(string username)
        {
            var user = await _userModelService.GetAsync(username: username);

            if (user is null)
            {
                return null;
            }

            return await CreateAsync(user);
        }

        public async Task<LoginTokenModel?> CreateAsync(Guid userModelId)
        {
            var user = await _userModelService.GetAsync(userModelId);

            if (user is null)
            {
                return null;
            }

            return await CreateAsync(user);
        }

        #endregion CREATE

        #region READ

        public async Task<LoginTokenModel?> GetAsync(int id)
        {
            return await _authenticationDbContext.LoginTokens.FindAsync(id);
        }

        public async Task<LoginTokenModel?> GetAsync(string loginTokenValue)
        {
            return await _authenticationDbContext.LoginTokens.FirstOrDefaultAsync(loginToken =>
                loginToken.Token.Equals(loginTokenValue));
        }

        public async Task<bool> Exists(string loginTokenValue)
        {
            return await GetAsync(loginTokenValue) is not null;
        }

        public async Task<bool> Exists(int id)
        {
            return await GetAsync(id) is not null;
        }

        public async Task<bool> IsValidAsync(string username, string loginToken)
        {
            var user = await _userModelService.GetAsync(username: username);
            if (user is null)
            {
                return false;
            }

            var queryToken =
                await _authenticationDbContext.LoginTokens.FirstOrDefaultAsync(token => token.Token.Equals(loginToken));
            if (queryToken is null)
            {
                return false;
            }

            if (queryToken.UserId != user.Id)
            {
                return false;
            }

            if (queryToken.IsExpired)
            {
                return false;
            }

            return true;
        }

        #endregion READ

        #region UPDATE

        public async Task<LoginTokenModel?> UpdateAsync(int id, LoginTokenModel loginTokenModel)
        {
            var queryLoginTokenModel = await GetAsync(id);

            if (queryLoginTokenModel is null)
            {
                return null;
            }

            queryLoginTokenModel.Token = loginTokenModel.Token.Equals(string.Empty) ? queryLoginTokenModel.Token : loginTokenModel.Token;
            queryLoginTokenModel.CreationDate = loginTokenModel.CreationDate.Equals(DateTime.MinValue) ? queryLoginTokenModel.CreationDate : loginTokenModel.CreationDate;
            queryLoginTokenModel.ExpirationDate = loginTokenModel.ExpirationDate.Equals(DateTime.MinValue) ? queryLoginTokenModel.ExpirationDate : loginTokenModel.ExpirationDate;

            await _authenticationDbContext.SaveChangesAsync();

            return queryLoginTokenModel;
        }

        public async Task<LoginTokenModel?> UpdateAsync(LoginTokenModel loginTokenModel)
        {
            return await UpdateAsync(loginTokenModel.Id, loginTokenModel);
        }

        public async Task<LoginTokenModel?> UpdateAsync(string loginTokenValue, LoginTokenModel loginTokenModel)
        {
            var queryLoginTokenModel = await GetAsync(loginTokenValue);

            if (queryLoginTokenModel is null)
            {
                return null;
            }

            return await UpdateAsync(queryLoginTokenModel.Id, loginTokenModel);
        }

        public async Task<LoginTokenModel?> ExpireAsync(string loginTokenValue)
        {
            var loginTokenModel = await GetAsync(loginTokenValue);

            if (loginTokenModel is null)
            {
                return null;
            }

            loginTokenModel.IsExpired = true;
            await _authenticationDbContext.SaveChangesAsync();

            return loginTokenModel;
        }

        public async Task<LoginTokenModel?> ExpireAsync(int id)
        {
            var loginTokenModel = await GetAsync(id);

            if (loginTokenModel is null)
            {
                return null;
            }

            loginTokenModel.IsExpired = true;
            await _authenticationDbContext.SaveChangesAsync();

            return loginTokenModel;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<LoginTokenModel?> DeleteAsync(int id)
        {
            var loginTokenModel = await GetAsync(id);

            if (loginTokenModel is null)
            {
                return null;
            }

            return await DeleteAsync(loginTokenModel);
        }

        public async Task<LoginTokenModel?> DeleteAsync(string loginTokenValue)
        {
            var loginTokenModel = await GetAsync(loginTokenValue);

            if (loginTokenModel is null)
            {
                return null;
            }

            return await DeleteAsync(loginTokenModel);
        }

        public async Task<LoginTokenModel?> DeleteAsync(LoginTokenModel loginTokenModel)
        {
            var returningLoginTokenModelEntity = _authenticationDbContext.LoginTokens.Remove(loginTokenModel);
            await _authenticationDbContext.SaveChangesAsync();

            return returningLoginTokenModelEntity.Entity;
        }

        #endregion DELETE

        #region PRIVATE METHODS

        private async Task<string> GenerateTokenAsync()
        {
            var loginToken = String.Empty;
            do
            {
                loginToken = _tokenGeneratorService.GenerateToken(LoginTokenModel.TOKEN_LENGTH);
            } while (await Exists(loginToken));

            _logger.LogInformation("Login Token generated: {loginToken}", loginToken);

            return loginToken;
        }

        #endregion PRIVATE METHODS

    }
}
