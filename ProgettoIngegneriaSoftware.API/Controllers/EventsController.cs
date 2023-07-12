using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ProgettoIngegneriaSoftware.API.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.API.Services.EventService;

namespace ProgettoIngegneriaSoftware.API.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{

    #region FIELDS

    private readonly ILogger<EventsController> _logger;
    private readonly IEventsService _eventsService;
    private readonly IAuthenticationService _authenticationService;

    #endregion FIELDS

    #region CTORS

    public EventsController(IEventsService eventsService, ILogger<EventsController> logger, IAuthenticationService authenticationService)
    {
        _eventsService = eventsService;
        _logger = logger;
        _authenticationService = authenticationService;
    }

    #endregion CTORS

    #region API ENDPOINTS

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("events", Name = "GetEvents")]
    public async Task<IActionResult> GetEvents()
    {
        if (!_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized("User not logged in.");

        return Ok(await _eventsService.GetEvents(_authenticationService.AuthenticatedUserId(HttpContext)));
    }

    #endregion API ENDPOINTS

}