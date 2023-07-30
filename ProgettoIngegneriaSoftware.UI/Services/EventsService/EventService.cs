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
    
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IList<IDisplayEvent>> GetEventsAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        throw new NotImplementedException();
    }
    
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IDisplayEvent?> GetEventAsync(Guid eventId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        throw new NotImplementedException();
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<bool> FollowEventAsync(Guid eventId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        throw new NotImplementedException();
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<bool> UnFollowEventAsync(Guid eventId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        throw new NotImplementedException();
    }

    #endregion IEventService IMP

}