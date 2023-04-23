using ProgettoIngegneriaSoftware.UI.Models;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public interface IEventsService
{
    public Task<List<ReadableEventModel>> GetEventsAsync();
    public Task<ReadableEventModel?> GetEventAsync(int id);
    public Task<bool> FollowEventAsync(int eventId);
    public Task<bool> UnFollowEventAsync(int eventId);
}