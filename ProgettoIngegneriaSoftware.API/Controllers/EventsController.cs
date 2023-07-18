using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.API.Services.EventService;
using ProgettoIngegneriaSoftware.API.Services.EventService.Exceptions;

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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("", Name = "GetEvents")]
    public async Task<IActionResult> GetEvents()
    {
        if (!_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized("User not logged in.");

        return Ok(await _eventsService.GetEvents(_authenticationService.AuthenticatedUserId(HttpContext)));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("event", Name = "GetEvent")]
    public async Task<IActionResult> GetEvent([FromQuery] Guid eventId)
    {
        if (!_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized("User not logged in.");

        EventResult? requestedEvent = null;

        try
        {
            requestedEvent = await _eventsService.GetEvent(eventId, _authenticationService.AuthenticatedUserId(HttpContext));
        }
        catch(EventNotFoundException)
        {
            return NotFound();
        }
        return Ok(requestedEvent);
    }

    #endregion API ENDPOINTS

}