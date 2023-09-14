namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public class EventBookingService : IEventBookingService
{
    #region FIELDS

    private readonly ApiHttpClient.ApiHttpClient _apiHttpClient;

    #endregion FIELDS

    #region CTORS

    public EventBookingService(ApiHttpClient.ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    #endregion CTORS

    #region IEventBookingService IMPLEMENTATION

    public async Task BookTicket(Guid eventId, ICollection<Guid> seatIds) => await _apiHttpClient.BookEvent(eventId, seatIds);

    #endregion IEventBookingService IMPLEMENTATION

}