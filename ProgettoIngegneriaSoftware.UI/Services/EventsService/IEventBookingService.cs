namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public interface IEventBookingService
{
    public Task BookTicket(Guid eventId, ICollection<Guid> seatIds);

    public Task CancelBookedTickets(Guid eventId, params Guid[] seatIds);
}