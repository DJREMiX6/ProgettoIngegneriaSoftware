using ProgettoIngegneriaSoftware.API.Models.DB;

namespace ProgettoIngegneriaSoftware.API.Services.UserEntityModelService;

public interface IUserEntityModelService
{

    /// <summary>
    /// Creates an instance of <see cref="UserEntityModel"/> with the given properties.
    /// </summary>
    /// <param name="username">The Username of the <see cref="UserEntityModel"/>.</param>
    /// <param name="email">The Email of the <see cref="UserEntityModel"/></param>
    /// <param name="passwordHash">The PasswordHash of the <see cref="UserEntityModel"/>.</param>
    /// <param name="passwordSalt">The PasswordSalt of the <see cref="UserEntityModel"/>.</param>
    public Task CreateAsync(string username, string email, byte[] passwordHash, byte[] passwordSalt);

    /// <summary>
    /// Checks if a <see cref="UserEntityModel"/> with the given username exists.
    /// </summary>
    /// <param name="username">Username of the <see cref="UserEntityModel"/></param>
    /// <param name="email">Email of the <see cref="UserEntityModel"/></param>
    /// <returns>The <see cref="Guid"/> of the <see cref="UserEntityModel"/> found, otherwise an empty <see cref="Guid"/>.</returns>
    public Task<Guid> ExistsAsync(string email);

    /// <summary>
    /// Validate a <see cref="UserEntityModel"/> password.
    /// </summary>
    /// <param name="userId">The Id of the <see cref="UserEntityModel"/>.</param>
    /// <param name="password">The password to validate.</param>
    /// <returns><c>True</c> if the password is correct, otherwise <c>False</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the user with the given userId does not exist.</exception>
    public Task<bool> ValidateUserAsync(Guid userId, string password);
}