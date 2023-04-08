using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application.Abstraction;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application.Records;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<EventsController> _logger;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IEventModelService _eventModelService;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public EventsController(ILogger<EventsController> logger, IUserAuthenticationService userAuthenticationService, IEventModelService eventModelService)
        {
            _logger = logger;
            _userAuthenticationService = userAuthenticationService;
            _eventModelService = eventModelService;
        }

        #endregion CTORS
        
        #region HTTP ACTIONS

        #region CREATE

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("create", Name = "CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventModelRecord editableEventModelRecord)
        {
            //Needs authentication
            if (!IsUserAuthenticated())
            {
                return Unauthorized();
            }

            var createdEvent = await _eventModelService.CreateAsync(editableEventModelRecord, AuthenticatedUserId());
            if (createdEvent == null)
            {
                throw new Exception("Error while creating the event.");
            }
            
            return CreatedAtAction("GetEvent", new { eventId = createdEvent.Id }, null);
        }

        #endregion CREATE

        #region READ

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("", Name = "GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventModelService.GetAsync();
            if (events.Count == 0)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{eventId}", Name = "GetEvent")]
        public async Task<IActionResult> GetEvent(Guid eventId)
        {
            var readableEventModel = await _eventModelService.GetAsync(eventId);
            if (readableEventModel is null)
            {
                return NotFound();
            }

            return Ok(readableEventModel);
        }

        #endregion READ

        #region UPDATE



        #endregion UPDATE

        #region DELETE



        #endregion DELETE

        #endregion HTTP ACTIONS

        #region PRIVATE METHODS

        [NonAction]
        private bool IsUserAuthenticated() => _userAuthenticationService.IsUserAuthenticated(HttpContext);

        [NonAction]
        private Guid AuthenticatedUserId() => _userAuthenticationService.AuthenticatedUserId(HttpContext) ?? throw new NullReferenceException("Logged in user Id is null.");

        #endregion PRIVATE METHODS

    }
}
