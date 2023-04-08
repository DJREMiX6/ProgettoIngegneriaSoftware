using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public interface IEventsService
{
    public Task<List<EventModel>> GetEventsAsync();
}