using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Services.AuthenticationService;
using ProgettoIngegneriaSoftware.API.Services.EventBookingService;
using ProgettoIngegneriaSoftware.API.Services.EventService.Exceptions;

namespace ProgettoIngegneriaSoftware.API.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{

    #region FIELDS

    private readonly ILogger<BookingController> _logger;
    private readonly IEventBookingService _eventBookingService;
    private readonly IAuthenticationService _authenticationService;

    #endregion FIELDS

    #region CTORS

    public BookingController(IEventBookingService eventBookingService, ILogger<BookingController> logger, IAuthenticationService authenticationService)
    {
        _eventBookingService = eventBookingService;
        _logger = logger;
        _authenticationService = authenticationService;
    }

    #endregion CTORS

    #region API ENDPOINTS

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("event/{eventId:guid}", Name = "BookSeats")]
    public async Task<IActionResult> BookSeats([FromRoute]Guid eventId, [FromBody]Guid[] seatIds)
    {
        if (!_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized("User not logged in.");

        EventResult? bookedEvent;

        try
        {
            bookedEvent = await _eventBookingService.BookSeats(eventId, _authenticationService.AuthenticatedUserId(HttpContext), seatIds);
        }
        catch (EventNotFoundException)
        {
            return NotFound("Event not found.");
        }
        catch (ArgumentException)
        {
            return BadRequest("One or more seats are unavailable, please try again.");
        }
        catch (SeatUnavailableException)
        {
            return BadRequest("One or more seats are unavailable, please try again.");
        }

        return Ok(bookedEvent);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("event/{eventId:guid}", Name = "CancelBookedSeats")]
    public async Task<IActionResult> CancelBookedSeats([FromRoute] Guid eventId, [FromBody] Guid[] seatIds)
    {
        if (!_authenticationService.IsUserAuthenticated(HttpContext))
            return Unauthorized("User not logged in.");

        EventResult? bookedEvent;

        try
        {
            bookedEvent = await _eventBookingService.CancelBookedSeats(eventId, _authenticationService.AuthenticatedUserId(HttpContext), seatIds);
        }
        catch (EventNotFoundException)
        {
            return NotFound("Event not found.");
        }
        catch (ArgumentException)
        {
            return BadRequest("One or more errors occurred, please try again later.");
        }

        return Ok(bookedEvent);
    }

    #endregion API ENDPOINTS

}