using ProgettoIngegneriaSoftware.API.Models;

namespace ProgettoIngegneriaSoftware.API.Services.EventBookingService;

public interface IEventBookingService
{
    public Task<EventResult> BookSeats(Guid eventId, Guid userId, ICollection<Guid> seatsIds);
    public Task<EventResult> CancelBookedSeats(Guid eventId, Guid userId, ICollection<Guid> seatsIds);
}