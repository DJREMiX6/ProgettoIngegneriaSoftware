using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Abstraction;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Utils.Extensions;

namespace ProgettoIngegneriaSoftware.Services
{
    public class UserModelService : IUserModelService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly ILogger<UserModelService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public UserModelService(AuthenticationDbContext authenticationDbContext, 
            ILogger<UserModelService> logger,
            IPasswordHasher passwordHasher)
        {
            _authenticationDbContext = authenticationDbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        #endregion CTORS

        #region CREATE

        public async Task<IReadableUseModel?> CreateAsync(IEditableUserModel editableUserModel)
        {
            if (!editableUserModel.HasValidPassword())
            {
                return null;
            }

            if (await GetAsync(username: editableUserModel.Username) is not null)
            {
                return null;
            }

            if (await GetAsync(email: editableUserModel.Email) is not null)
            {
                return null;
            }

            var hashResult = await _passwordHasher.HashPassword(editableUserModel.Password);
            var user = new UserModel()
            {
                Username = editableUserModel.Username,
                Email = editableUserModel.Email,
                PasswordHash = hashResult.Hash,
                Salt = hashResult.Salt,
            };

            var addedUserEntity = await _authenticationDbContext.Users.AddAsync(user);
            await _authenticationDbContext.SaveChangesAsync();

            return addedUserEntity.Entity;
        }

        #endregion CREATE

        #region READ

        public async Task<IReadableUseModel?> GetAsync(Guid id)
        {
            return await _authenticationDbContext.Users.FindAsync(id);
        }

        public async Task<IReadableUseModel?> GetAsync(long confirmationToken)
        {
            if (!confirmationToken.IsValidConfirmationToken())
            {
                return null;
            }

            return await _authenticationDbContext.Users.FirstOrDefaultAsync(user =>
                user.ConfirmationToken.Equals(confirmationToken));
        }

        public async Task<IReadableUseModel?> GetAsync(string? username = null, string? email = null)
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
                return await _authenticationDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
            }

            return await _authenticationDbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        }

        public async Task<bool> ValidatePasswordAsync(Guid id, string password)
        {
            var user = (await GetAsync(id)) as UserModel;
            if (user is null)
            {
                return false;
            }

            return await _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt, useSecret: true);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await GetAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(string? username, string? email)
        {
            return await GetAsync(username: username, email: email) is not null;
        }

        #endregion READ

        #region UPDATE

        public async Task<IReadableUseModel?> UpdateAsync(Guid id, IEditableUserModel editableUserModel)
        {
            var userToUpdate = (await GetAsync(id)) as UserModel;
            if (userToUpdate is null)
            {
                return null;
            }

            userToUpdate.Username = editableUserModel.Username.Equals(string.Empty) ? userToUpdate.Username : editableUserModel.Username;
            userToUpdate.Email = editableUserModel.Email.Equals(string.Empty) ? userToUpdate.Email : editableUserModel.Email;
            userToUpdate.PasswordHash = editableUserModel.PasswordHash.Equals(Array.Empty<byte>()) ? userToUpdate.PasswordHash : editableUserModel.PasswordHash;
            userToUpdate.Salt = editableUserModel.Salt.Equals(Array.Empty<byte>()) ? userToUpdate.Salt : editableUserModel.Salt;

            await _authenticationDbContext.SaveChangesAsync();

            return userToUpdate;
        }

        public async Task<UserModel?> ConfirmAsync(long confirmationToken)
        {
            var userToConfirm = await GetAsync(confirmationToken);

            if (userToConfirm is null)
            {
                return null;
            }

            if (userToConfirm.ConfirmationToken == 0)
            {
                return null;
            }
            
            userToConfirm.ConfirmationToken = 0;

            await _authenticationDbContext.SaveChangesAsync();

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

            _authenticationDbContext.Users.Remove(userToDelete);
            await _authenticationDbContext.SaveChangesAsync();

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
            if (!confirmationToken.IsValidConfirmationToken())
            {
                return false;
            }

            var queryUser = await GetAsync(confirmationToken);
            //If there is no user with the same confirmation token then it is a valid confirmation token
            return queryUser is null;
        }

        private async Task<bool> IsValidInStorage(UserModel userModel)
        {
            return await _authenticationDbContext.Users.ContainsAsync(userModel);
        }

        #endregion PRIVATE METHODS

    }
}
