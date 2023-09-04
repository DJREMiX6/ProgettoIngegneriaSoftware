using Newtonsoft.Json;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Helpers;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public class EventService : IEventsService
{

    #region FIELDS

    private readonly ApiHttpClient.ApiHttpClient _apiHttpClient;
    private readonly JsonSerializerSettings _serializerSettings;

    #endregion FIELDS

    #region CTORS

    public EventService(ApiHttpClient.ApiHttpClient apiHttpClient, JsonSerializerSettings serializerSettings)
    {
        _apiHttpClient = apiHttpClient;
        _serializerSettings = serializerSettings;
    }

    #endregion CTORS

    #region IEventService IMP
    
    public async Task<List<IDisplayEvent>> GetEventsAsync()
    {
        var eventsResultData =  await _apiHttpClient.GetEvents();
        var events = JsonConvert.DeserializeObject<List<IEventResult>>(eventsResultData, _serializerSettings).Cast<IDisplayEvent>().ToList();
        return events;
    }

    public async Task<IDisplayEvent?> GetEventAsync(Guid eventId)
    {
        var eventData = await _apiHttpClient.GetEvent(eventId);
        if (string.IsNullOrWhiteSpace(eventData))
            return null;
        var deserializedEventData = (IDisplayEvent)JsonConvert.DeserializeObject<IEventResult>(eventData, _serializerSettings);
        return deserializedEventData;
    }

    #endregion IEventService IMP

    

}