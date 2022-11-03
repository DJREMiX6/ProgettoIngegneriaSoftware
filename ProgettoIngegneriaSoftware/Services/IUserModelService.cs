using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;

namespace ProgettoIngegneriaSoftware.Services
{
    public interface IUserModelService
    {

        #region CREATE

        /// <summary>
        /// Asynchronously creates a new instance of <see cref="UserModel"/> and adds it to the data source.
        /// </summary>
        /// <param name="username">The user's Username</param>
        /// <param name="email">The user's email</param>
        /// <param name="password">The user's validated password</param>
        /// <returns>the created instance of <see cref="UserModel"/> or <c>null</c> if something went wrong.</returns>
        public Task<UserModel?> CreateAsync(string username, string email, string password);
        
        public Task<UserModel?> CreateAsync(string username, string email, byte[] passwordHash, byte[] salt);
        
        public Task<UserModel?> CreateAsync(UserModelRecord userModelRecord);

        #endregion CREATE

        #region READ

        /// <summary>
        /// Asynchronously gets an instance of <see cref="UserModel"/> with a specific Id from the data source.
        /// </summary>
        /// <param name="id">The <see cref="UserModel"/> instance Id</param>
        /// <returns>the <see cref="UserModel"/> instance with the specified Id or <c>null</c> if no <see cref="UserModel"/> with that specific Id has been found.</returns>
        public Task<UserModel?> GetAsync(Guid id);
        /// <summary>
        /// Asynchronously gets an instance of <see cref="UserModel"/> with a specific ConfirmationToken from the data source.
        /// </summary>
        /// <param name="confirmationToken">The user's ConfirmationToken</param>
        /// <returns>the <see cref="UserModel"/> instance with the specified ConfirmationToken or <c>null</c> if no <see cref="UserModel"/> with that specific ConfirmationToken has been found.</returns>
        public Task<UserModel?> GetAsync(long confirmationToken);
        /// <summary>
        /// Asynchronously gets an instance of <see cref="UserModel"/> with a specific Username or Email from the data source.
        /// </summary>
        /// <param name="username">The user's username used to filter the data source.</param>
        /// <param name="email">The user's email used to filter the data source.</param>
        /// <returns>the <see cref="UserModel"/> instance with the specified Username or Email,
        /// or <c>null</c> if no <see cref="UserModel"/> with that specific Username has been found.</returns>
        public Task<UserModel?> GetAsync(string? username = null, string? email = null);

        public Task<bool> ValidateAsync(string username, string password); 
        public Task<bool> ExistsAsync(Guid id);
        public Task<bool> ExistsAsync(string? username = null, string? email = null);

        #endregion READ

        #region UPDATE

        /// <summary>
        /// Asynchronously updates a <see cref="UserModel"/> with a specific Id with data from another <see cref="UserModel"/> instance (Id cannot be modified).
        /// </summary>
        /// <param name="id">The <see cref="UserModel"/>'s Id to filter the data source.</param>
        /// <param name="userModel">The <see cref="UserModel"/> instance that holds the updated properties values.</param>
        /// <returns>the <see cref="UserModel"/> updated instance from the data source or <c>null</c> if no <see cref="UserModel"/> with that Id has been found.</returns>
        public Task<UserModel?> UpdateAsync(Guid id, UserModel userModel);
        /// <summary>
        /// Asynchronously updates a <see cref="UserModel"/> with a specific Id.
        /// </summary>
        /// <param name="id">The <see cref="UserModel"/>'s Id to filter the data source.</param>
        /// <param name="username">The user's username</param>
        /// <param name="email">The user's email</param>
        /// <param name="password">The user's validated password</param>
        /// <returns>the updated <see cref="UserModel"/> instance from the data source
        /// or <c>null</c> if no <see cref="UserModel"/> with that specific Id has been found.</returns>
        public Task<UserModel?> UpdateAsync(Guid id, string? username = null, string? email = null, string? password = null);
        /// <summary>
        /// Asynchronously updates a <see cref="UserModel"/> with a specific Id.
        /// </summary>
        /// <param name="id">The <see cref="UserModel"/>'s Id to filter the data source.</param>
        /// <param name="username">The user's username</param>
        /// <param name="email">The user's email</param>
        /// <param name="passwordHash">The user's validated and hashed password</param>
        /// <param name="salt">The salt used to hash the user's password</param>
        /// <returns>the updated <see cref="UserModel"/> instance from the data source
        /// or <c>null</c> if no <see cref="UserModel"/> with that specific Id has been found.</returns>
        public Task<UserModel?> UpdateAsync(Guid id, string? username = null, string? email = null, byte[]? passwordHash = null, byte[]? salt = null);
        /// <summary>
        /// Asynchronously sets the <see cref="UserModel"/> IsConfirmed property to <c>true</c> and its ConfirmationToken to <c>null</c>.
        /// </summary>
        /// <param name="confirmationToken">The ConfirmationToken to filter the data source.</param>
        /// <returns>The confirmed <see cref="UserModel"/></returns> instance
        /// or <c>null</c> if no <see cref="UserModel"/> with that specific ConfirmationToken has been found.
        public Task<UserModel?> ConfirmAsync(long confirmationToken);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Asynchronously deletes a <see cref="UserModel"/> with a specific Id from the data source.
        /// </summary>
        /// <param name="id">The <see cref="UserModel"/> Id</param>
        /// <returns>the deleted <see cref="UserModel"/> instance or <c>null</c> if no <see cref="UserModel"/> with that specific Id has been found.</returns>
        public Task<UserModel?> DeleteAsync(Guid id);
        /// <summary>
        /// Asynchronously deletes a <see cref="UserModel"/> from the data source.
        /// </summary>
        /// <param name="userModel">The <see cref="UserModel"/> To remove</param>
        /// <returns>the deleted <see cref="UserModel"/> instance or <c>null</c> if no <see cref="UserModel"/> with that specific Id has been found.</returns>
        public Task<UserModel?> DeleteAsync(UserModel userModel);
        /// <summary>
        /// Asynchronously deletes a <see cref="UserModel"/> instance from the data source by its Username property.
        /// </summary>
        /// <param name="username">The user's username property value to filter the data source.</param>
        /// <returns>The deleted <see cref="UserModel"/> instance
        /// or <c>null</c> if no <see cref="UserModel"/> with that Username property value has been found.</returns>
        public Task<UserModel?> DeleteByUsernameAsync(string username);
        /// <summary>
        /// Asynchronously deletes a <see cref="UserModel"/> instance from the data source by its Email property.
        /// </summary>
        /// <param name="email">The user's email property value to filter the data source.</param>
        /// <returns>The deleted <see cref="UserModel"/> instance
        /// or <c>null</c> if no <see cref="UserModel"/> with that Email property value has been found.</returns>
        public Task<UserModel?> DeleteByEmailAsync(string email);

        #endregion DELETE

    }
}
