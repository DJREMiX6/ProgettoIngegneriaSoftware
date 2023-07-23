using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public interface IEventsService
{
    public Task<IList<IDisplayEvent>> GetEventsAsync();
    public Task<IDisplayEvent?> GetEventAsync(Guid eventId);
    public Task<bool> FollowEventAsync(Guid eventId);
    public Task<bool> UnFollowEventAsync(Guid eventId);
}