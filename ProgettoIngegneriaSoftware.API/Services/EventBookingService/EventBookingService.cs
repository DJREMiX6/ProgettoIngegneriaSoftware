using Microsoft.Extensions.Logging;
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

    public Task<EventResult> BookSeats(Guid eventId, Guid userId, ICollection<Guid> seatsIds) =>
        Task.Run(() =>
        {
            EnsureValidEventId(eventId);
            EnsureValidSeatsIds(seatsIds);
            EnsureSeatsNotAlreadyBooked(eventId, seatsIds);
            AddBookedSeatsToDb(eventId, userId, seatsIds);

            return GetUpdatedEvent(eventId, userId);
        });

    public Task<EventResult> CancelBookedSeats(Guid eventId, Guid userId, ICollection<Guid> seatsIds) =>
        Task.Run(() =>
        {
            EnsureValidEventId(eventId);
            EnsureValidSeatsIds(seatsIds);
            EnsureSeatsBookedByUser(eventId, userId, seatsIds);
            RemoveBookedSeatsFromDb(eventId, userId, seatsIds);

            return GetUpdatedEvent(eventId, userId);
        });

    #endregion IEventBooking IMPLEMENTATION

    #region METHODS

    private void EnsureValidEventId(Guid eventId)
    {
        var eventExists = _applicationDbContext.Events.Any(eventEntityModel => eventEntityModel.Id == eventId);
        if (!eventExists)
            throw new EventNotFoundException(eventId);
    }

    private void EnsureValidSeatsIds(ICollection<Guid> seatsIds)
    {
        var validSeatsFromDb = _applicationDbContext.Seats.Count(seat => seatsIds.Contains(seat.Id));
        if (validSeatsFromDb != seatsIds.Count)
            throw new ArgumentException("One of the seats selected does not exists.");
    }

    private void EnsureSeatsNotAlreadyBooked(Guid eventId, ICollection<Guid> seatsIds)
    {
        var seatAlreadyBooked = _applicationDbContext.BookedSeats
            .FirstOrDefault(bookedSeat => seatsIds.Contains(bookedSeat.SeatId) 
                                          && bookedSeat.EventId == eventId);
        if (seatAlreadyBooked != null)
            throw new SeatUnavailableException(seatAlreadyBooked.SeatId);
    }

    private void EnsureSeatsBookedByUser(Guid eventId, Guid userId, ICollection<Guid> seatsIds)
    {
        var seatsBookedByUser = _applicationDbContext.BookedSeats
            .Count(bookedSeatEntityModel => bookedSeatEntityModel.EventId == eventId
                                            && bookedSeatEntityModel.UserId == userId
                                            && seatsIds.Contains(bookedSeatEntityModel.SeatId));
        if (seatsBookedByUser != seatsIds.Count)
            throw new ArgumentException("One of the seats selected is not booked by this user.");
    }

    private void AddBookedSeatsToDb(Guid eventId, Guid userId, ICollection<Guid> seatIds)
    {
        foreach (var seatId in seatIds)
            _applicationDbContext.BookedSeats.Add(CreateBookedSeatEntityModel(eventId, seatId, userId));
        _applicationDbContext.SaveChanges();
    }

    private void RemoveBookedSeatsFromDb(Guid eventId, Guid userId, ICollection<Guid> seatsIds)
    {
        var bookedSeatsToRemove = _applicationDbContext.BookedSeats
            .Where(bookedSeatEntityModel => bookedSeatEntityModel.EventId == eventId
                                            && bookedSeatEntityModel.UserId == userId
                                            && seatsIds.Contains(bookedSeatEntityModel.SeatId));

        _applicationDbContext.BookedSeats.RemoveRange(bookedSeatsToRemove);
        _applicationDbContext.SaveChanges();
    }

    private EventResult GetUpdatedEvent(Guid eventId, Guid userId) => _eventsService.GetEvent(eventId, userId).Result;

    private static BookedSeatEntityModel CreateBookedSeatEntityModel(Guid eventId, Guid seatId, Guid userId) =>
        new()
        {
            EventId = eventId,
            SeatId = seatId,
            UserId = userId
        };

    #endregion METHODS

}