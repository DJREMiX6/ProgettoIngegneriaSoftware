using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Models.DB;
using ProgettoIngegneriaSoftware.API.Security;
using ProgettoIngegneriaSoftware.API.Services.RegistrationService.Exceptions;
using ProgettoIngegneriaSoftware.API.Services.UserEntityModelService;

namespace ProgettoIngegneriaSoftware.API.Services.RegistrationService;

public class RegistrationService : IRegistrationService
{

    #region FIELDS

    private ILogger<RegistrationService> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserEntityModelService _userEntityModelService;

    #endregion FIELDS

    #region CTORS

    public RegistrationService(ILogger<RegistrationService> logger, ApplicationDbContext dbContext, IPasswordHasher passwordHasher, IUserEntityModelService userEntityModelService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userEntityModelService = userEntityModelService;
    }

    #endregion CTORS

    #region IRegistrationService IMPLEMENTATION

    public async Task RegisterUser(RegisterUserInfo registerUserInfo)
    {
        await EnsureUserUniqueness(registerUserInfo);
        EnsurePasswordValidity(registerUserInfo);
        await CreateUser(registerUserInfo);
    }

    #endregion IRegistrationService IMPLEMENTATION

    #region METHODS

    private async Task EnsureUserUniqueness(RegisterUserInfo registerUserInfo)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Email.Equals(registerUserInfo.Email) || user.Username.Equals(registerUserInfo.UserName));

        if (existingUser != null)
            if (existingUser.Email.Equals(registerUserInfo.Email))
                throw new EmailAlreadyInUseException();
            else
                throw new UsernameAlreadyInUseException();
    }

    private void EnsurePasswordValidity(RegisterUserInfo registerUserInfo)
    {
        if (!registerUserInfo.Password.Equals(registerUserInfo.ConfirmPassword))
            throw new InvalidPasswordException();
    }

    private async Task CreateUser(RegisterUserInfo registerUserInfo)
    {
        var passwordHashResult = await _passwordHasher.HashPassword(registerUserInfo.Password);
        await _userEntityModelService.CreateAsync(registerUserInfo.UserName, registerUserInfo.Email,
            passwordHashResult.Hash!, passwordHashResult.Salt!);
    }

    #endregion METHODS

}