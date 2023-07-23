using ProgettoIngegneriaSoftware.UI.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public class EventService : IEventsService
{

    #region FIELDS



    #endregion FIELDS

    #region CTORS

    public EventService()
    {
        
    }

    #endregion CTORS

    #region IEventService IMP

    public async Task<IList<IDisplayEvent>> GetEventsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IDisplayEvent?> GetEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> FollowEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnFollowEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    #endregion IEventService IMP

}