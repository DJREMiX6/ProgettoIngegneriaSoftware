using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API.Extensions;
using ProgettoIngegneriaSoftware.API.Models.DB;
using ProgettoIngegneriaSoftware.API.Security;

namespace ProgettoIngegneriaSoftware.API.Services.UserEntityModelService;

public class EntityFrameworkUserEntityModelService : IUserEntityModelService
{

    #region FIELDS

    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IPasswordHasher _passwordHasher;

    #endregion FIELDS

    #region CTORS

    public EntityFrameworkUserEntityModelService(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
    {
        _applicationDbContext = applicationDbContext;
        _passwordHasher = passwordHasher;
    }

    #endregion CTORS

    #region IUserEntityModelService IMPLEMENTATION

    public async Task CreateAsync(string username, string email, byte[] passwordHash, byte[] passwordSalt)
    {
        await _applicationDbContext.Users
            .AddAsync(new UserEntityModel()
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<Guid> ExistsAsync(string email) =>
        (await _applicationDbContext.Users.FirstOrDefaultAsync(userEntityModel => userEntityModel.Email == email))?.Id ?? Guid.Empty;

    public async Task<bool> ValidateUserAsync(Guid userId, string password)
    {
        var user = await _applicationDbContext.Users.FindAsync(userId);

        if (user == null)
            throw new ArgumentException("No user found.");

        return await _passwordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
    }

    #endregion IUserEntityModelService IMPLEMENTATION

}