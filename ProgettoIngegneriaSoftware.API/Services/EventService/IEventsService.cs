using ProgettoIngegneriaSoftware.API.Models;

namespace ProgettoIngegneriaSoftware.API.Services.EventService;

public interface IEventsService
{
    public Task<IList<EventResult>> GetEvents(Guid userId);
    public Task<EventResult> GetEvent(Guid eventId, Guid userId);
}