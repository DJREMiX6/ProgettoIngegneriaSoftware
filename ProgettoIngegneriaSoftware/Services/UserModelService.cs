using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication.Records;
using ProgettoIngegneriaSoftware.Security;

namespace ProgettoIngegneriaSoftware.Services
{
    public class UserModelService : IUserModelService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly AutenticationDbContext _autenticationDbContext;
        private readonly ILogger<UserModelService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public UserModelService(AutenticationDbContext autenticationDbContext, 
            ILogger<UserModelService> logger,
            IPasswordHasher passwordHasher)
        {
            _autenticationDbContext = autenticationDbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        #endregion CTORS

        #region CREATE

        public async Task<UserModel?> CreateAsync(string username, string email, string password)
        {
            var hashResult = await _passwordHasher.HashPassword(password, null, true);
            return await CreateAsync(username, email, hashResult.Hash, hashResult.Salt);
        }

        public async Task<UserModel?> CreateAsync(string username, string email, byte[] passwordHash, byte[] salt)
        {
            long confirmationToken = await CreateConfirmationToken();
            var newUserModel = new UserModel()
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt,
                IsConfirmed = false,
                ConfirmationToken = confirmationToken
            };
            return await CreateAsync(newUserModel);
        }

        public async Task<UserModel?> CreateAsync(UserModelRecord userModelRecord)
        {
            if (!userModelRecord.IsValidPassword)
            {
                return null;
            }

            return await CreateAsync(userModelRecord.Username, userModelRecord.Email, userModelRecord.Password);
        }

        #endregion CREATE

        #region READ

        public async Task<UserModel?> GetAsync(Guid id)
        {
            return await _autenticationDbContext.Users.FindAsync(id);
        }

        public async Task<UserModel?> GetAsync(long confirmationToken)
        {
            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user =>
                user.ConfirmationToken.Equals(confirmationToken));
        }

        public async Task<UserModel?> GetAsync(string? username = null, string? email = null)
        {
            if (username is null && email is null)
            {
                return null;
            }

            if (username is null && email.Trim().Equals(String.Empty))
            {
                return null;
            }

            if (email is null && username.Trim().Equals(String.Empty))
            {
                return null;
            }

            if (username is null)
            {
                return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
            }

            return await _autenticationDbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetAsync(id) is not null;
        }

        public async Task<bool> Exists(string? username, string? email)
        {
            return await GetAsync(username: username, email: email) is not null;
        }

        #endregion READ

        #region UPDATE

        public async Task<UserModel?> UpdateAsync(Guid id, UserModel userModel)
        {
            var userToUpdate = await GetAsync(id);
            if (userToUpdate is null)
            {
                return null;
            }

            userToUpdate.Username = userModel.Username.Equals(string.Empty) ? userToUpdate.Username : userModel.Username;
            userToUpdate.Email = userModel.Email.Equals(string.Empty) ? userToUpdate.Email : userModel.Email;
            userToUpdate.PasswordHash = userModel.PasswordHash.Equals(Array.Empty<byte>()) ? userToUpdate.PasswordHash : userModel.PasswordHash;
            userToUpdate.Salt = userModel.Salt.Equals(Array.Empty<byte>()) ? userToUpdate.Salt : userModel.Salt;

            await _autenticationDbContext.SaveChangesAsync();

            return userToUpdate;
        }

        public async Task<UserModel?> UpdateAsync(Guid id, string? username = null, string? email = null, string? password = null)
        {
            HashResult? hashResult = null;
            if (password is not null)
            {
                hashResult = await _passwordHasher.HashPassword(password, null, true);
            }

            return await UpdateAsync(id, username, email, hashResult?.Hash, hashResult?.Salt);
        }

        public async Task<UserModel?> UpdateAsync(Guid id, string? username = null, string? email = null, byte[]? passwordHash = null,
            byte[]? salt = null)
        {
            var userToUpdate = new UserModel()
            {
                Username = username ?? string.Empty,
                Email = email ?? string.Empty,
                PasswordHash = passwordHash ?? Array.Empty<byte>(),
                Salt = salt ?? Array.Empty<byte>()
            };

            return await UpdateAsync(id, userToUpdate);
        }

        public async Task<UserModel?> ConfirmAsync(long confirmationToken)
        {
            var userToConfirm = await GetAsync(confirmationToken);

            if (userToConfirm is null)
            {
                return null;
            }

            userToConfirm.IsConfirmed = true;
            userToConfirm.ConfirmationToken = 0;

            await _autenticationDbContext.SaveChangesAsync();

            return userToConfirm;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<UserModel?> DeleteAsync(Guid id)
        {
            var userToDelete = await GetAsync(id);

            if (userToDelete is null)
            {
                return null;
            }

            _autenticationDbContext.Users.Remove(userToDelete);
            await _autenticationDbContext.SaveChangesAsync();

            return userToDelete;
        }

        public async Task<UserModel?> DeleteAsync(UserModel userModel)
        {
            return await DeleteAsync(userModel.Id);
        }

        public async Task<UserModel?> DeleteByUsernameAsync(string username)
        {
            var userToDelete = await GetAsync(username: username);

            if (userToDelete is null)
            {
                return null;
            }

            return await DeleteAsync(userToDelete);
        }

        public async Task<UserModel?> DeleteByEmailAsync(string email)
        {
            var userToDelete = await GetAsync(email: email);

            if (userToDelete is null)
            {
                return null;
            }

            return await DeleteAsync(userToDelete);
        }

        #endregion DELETE

        #region PRIVATE METHODS

        private async Task<UserModel?> CreateAsync(UserModel userModel)
        {

            if (await GetAsync(username: userModel.Username) is not null)
            {
                return null;
            }

            if (await GetAsync(email: userModel.Email) is not null)
            {
                return null;
            }

            userModel.Id = Guid.Empty;

            var addedUserEntity = await _autenticationDbContext.Users.AddAsync(userModel);
            await _autenticationDbContext.SaveChangesAsync();

            return addedUserEntity.Entity;
        }

        private async Task<long> CreateConfirmationToken()
        {
            long confirmationToken = 0;
            do
            {
                confirmationToken = Random.Shared.NextInt64();
            } while(!await IsNewConfirmationTokenValid(confirmationToken));

            _logger.LogInformation("Confirmation Token generated: {confirmationToken}", confirmationToken);
            return confirmationToken;
        }

        private async Task<bool> IsNewConfirmationTokenValid(long confirmationToken)
        {
            if (confirmationToken <= 123456)
            {
                return false;
            }

            var queryUser = await GetAsync(confirmationToken);
            //If there is no user with the same confirmation token then it is a valid confirmation token
            return queryUser is null;
        }

        #endregion PRIVATE METHODS

    }
}
