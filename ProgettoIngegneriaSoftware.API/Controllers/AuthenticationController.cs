using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.API.Services.RegistrationService;
using ProgettoIngegneriaSoftware.API.Services.RegistrationService.Exceptions;

namespace ProgettoIngegneriaSoftware.API.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{

    #region FIELDS

    private readonly ILogger<AuthenticationController> _logger;
    private readonly IRegistrationService _registrationService;
    private readonly IAuthenticationService _authenticationService;

    #endregion FIELDS

    #region CTORS

    public AuthenticationController(ILogger<AuthenticationController> logger, IRegistrationService registrationService, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _registrationService = registrationService;
        _authenticationService = authenticationService;
    }

    #endregion CTORS

    #region API ENDPOINTS

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("signup", Name = "Register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserInfo registerUserInfo)
    {
        if (_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = "User authenticated. Log out first.",
                Status = StatusCodes.Status400BadRequest
            });

        try
        {
            await _registrationService.RegisterUser(registerUserInfo);
        }
        catch (UsernameAlreadyInUseException usernameAlreadyInUseException)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = usernameAlreadyInUseException.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (EmailAlreadyInUseException emailAlreadyInUseException)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = emailAlreadyInUseException.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (InvalidPasswordException invalidPasswordException)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = invalidPasswordException.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Unexpected error.",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok("User registered correctly.");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("signin", Name="SignIn")]
    public async Task<IActionResult> SignIn([FromBody]LoginUserInfo loginUserInfo)
    {
        if (_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = "User already logged in.",
                Status = StatusCodes.Status400BadRequest
            });

        try
        {
            await _authenticationService.SignInAsync(HttpContext, loginUserInfo);
        }
        catch (AuthenticationException aex)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = aex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Unexpected error.",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok("User authenticated.");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("signout", Name = "SignOut")]
    public new async Task<IActionResult> SignOut()
    {
        if (_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized(new ProblemDetails()
            {
                Title = "Authentication error.",
                Detail = "User not logged in.",
                Status = StatusCodes.Status400BadRequest
            });

        try
        {
            await _authenticationService.SignOutAsync(HttpContext);
        }
        catch (Exception ex)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Unexpected error.",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok("User signed out.");
    }

    #endregion API ENDPOINTS

}