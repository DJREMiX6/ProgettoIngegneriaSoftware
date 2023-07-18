namespace ProgettoIngegneriaSoftware.API.Services.EventService.Exceptions;

public class EventNotFoundException : Exception
{

    private const string EVENT_NOT_FOUND_EXCEPTION_MESSAGE = "The event with id :{0} has not been found.";

    public EventNotFoundException(Guid eventId) : base(string.Format(EVENT_NOT_FOUND_EXCEPTION_MESSAGE, eventId)) {}
}