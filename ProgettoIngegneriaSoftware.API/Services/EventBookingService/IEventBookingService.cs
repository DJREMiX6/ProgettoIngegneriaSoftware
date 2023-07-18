using ProgettoIngegneriaSoftware.API.Models;

namespace ProgettoIngegneriaSoftware.API.Services.EventBookingService;

public interface IEventBookingService
{
    public Task<EventResult> BookSeats(Guid eventId, Guid userId, IList<Guid> seatsIds);
}