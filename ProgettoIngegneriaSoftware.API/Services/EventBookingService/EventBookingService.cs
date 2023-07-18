using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Models.DB;
using ProgettoIngegneriaSoftware.API.Services.EventService;
using ProgettoIngegneriaSoftware.API.Services.EventService.Exceptions;
using System.Linq;

namespace ProgettoIngegneriaSoftware.API.Services.EventBookingService;

public class EventBookingService : IEventBookingService
{

    #region FIELDS

    private readonly ILogger<EventBookingService> _logger;
    private readonly IEventsService _eventsService;
    private readonly ApplicationDbContext _applicationDbContext;

    #endregion FIELDS

    #region CTORS

    public EventBookingService(ILogger<EventBookingService> logger, ApplicationDbContext applicationDbContext, IEventsService eventsService)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
        _eventsService = eventsService;
    }

    #endregion CTORS

    #region IEventBookingService IMPLEMENTATION

    public Task<EventResult> BookSeats(Guid eventId, Guid userId, IList<Guid> seatsIds) =>
        Task.Run(() =>
        {
            var eventExists = _applicationDbContext.Events.Any(eventEntityModel => eventEntityModel.Id == eventId);
            if (!eventExists)
                throw new EventNotFoundException(eventId);

            var seatExists = _applicationDbContext.Seats.Any(seat => seatsIds.Contains(seat.Id));
            if (!seatExists)
                throw new ArgumentException($"One of the seats selected does not exists.");

            var seatAlreadyBooked = _applicationDbContext.BookedSeats.FirstOrDefault(bookedSeat => seatsIds.Contains(bookedSeat.SeatId) && bookedSeat.EventId == eventId);
            if (seatAlreadyBooked != null)
                throw new SeatUnavailableException(seatAlreadyBooked.SeatId);

            foreach(var seatId in seatsIds)
                _applicationDbContext.BookedSeats.Add(new BookedSeatEntityModel()
                {
                    EventId = eventId,
                    SeatId = seatId,
                    UserId = userId
                });
            _applicationDbContext.SaveChanges();
            //TODO CHECK Y IT BOOKS OTHER SEATS MORE THAN THE ASKED ONES
            return _eventsService.GetEvent(eventId, userId).Result;
        });

    #endregion IEventBooking IMPLEMENTATION

}