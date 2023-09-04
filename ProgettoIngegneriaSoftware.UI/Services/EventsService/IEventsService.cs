using ProgettoIngegneriaSoftware.UI.Models;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public interface IEventsService
{
    public Task<List<IDisplayEvent>> GetEventsAsync();
    public Task<IDisplayEvent?> GetEventAsync(Guid eventId);
}