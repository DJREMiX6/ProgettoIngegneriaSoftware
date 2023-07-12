using ProgettoIngegneriaSoftware.API.Models;
using ProgettoIngegneriaSoftware.API.Models.DB;
using ProgettoIngegneriaSoftware.API.Models.HelperModels;

namespace ProgettoIngegneriaSoftware.API.Services.EventService;

public class EventService : IEventsService
{

    #region FIELDS

    private readonly ILogger<EventService> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    #endregion FIELDS

    #region CTORS

    public EventService(ApplicationDbContext applicationDbContext, ILogger<EventService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    #endregion CTORS

    #region  IEventService IMPLEMENTATION

    public async Task<IList<EventResult>> GetEvents(Guid userId)
    {
        var eventsFromDb = _applicationDbContext.Events.ToList();
        var events = new List<EventResult>();
        foreach (var eventFromDb in eventsFromDb)
        {
            var placeName = GetPlaceEntityModel(eventFromDb.PlaceId).Name;
            var totalSeatsCount = GetTotalSeatsCount(eventFromDb);
            var availableSeats = GetAvailableSeats(eventFromDb);
            var bookedSeat = GetUserBookedSeat(eventFromDb, userId);
            events.Add(new EventResult()
            {
                Name = eventFromDb.Name,
                Description = eventFromDb.Description,
                Date = eventFromDb.Date,
                Location = placeName,
                ImageSource = eventFromDb.ImageSrc,
                TotalSeatsCount = totalSeatsCount,
                AvailableSeats = availableSeats,
                BookedSeat = bookedSeat
            });
        }

        return events;
    }

    public async Task<EventResult> GetEvent(Guid eventId, Guid userId)
    {
        throw new NotImplementedException();
    }

    #endregion IEventService IMPLEMENTATION

    #region METHODS

    private PlaceEntityModel GetPlaceEntityModel(Guid placeId) => _applicationDbContext.Places.Find(placeId)!;

    private IList<SeatsZonesSeatsRowsSeatsJoinedTableEntity> GetTotalSeats(EventEntityModel eventFromDb) =>
        _applicationDbContext.SeatsZones
            .Join(
                _applicationDbContext.SeatsRows,
                seatsZoneEntityModel => seatsZoneEntityModel.Id,
                seatsRowEntityModel => seatsRowEntityModel.SeatsZoneId,
                (seatsZoneEntityModel, seatsRowEntityModel) => new SeatsZonesSeatsRowsJoinedTableEntity()
                {
                    PlaceId = seatsZoneEntityModel.PlaceId,
                    SeatsZoneId = seatsZoneEntityModel.Id,
                    SeatsZoneName = seatsZoneEntityModel.Name,
                    SeatsRowId = seatsRowEntityModel.Id,
                    SeatsRowName = seatsRowEntityModel.Name
                })
            .Join(_applicationDbContext.Seats,
                seatsZoneRowJoinedTableEntityModel => seatsZoneRowJoinedTableEntityModel.SeatsRowId,
                seatsEntityModel => seatsEntityModel.SeatsRowId,
                (seatsZoneRowJoinedTableEntity, seatsEntityModel) => new SeatsZonesSeatsRowsSeatsJoinedTableEntity()
                {
                    PlaceId = seatsZoneRowJoinedTableEntity.PlaceId,
                    SeatsZoneId = seatsZoneRowJoinedTableEntity.SeatsZoneId,
                    SeatsZoneName = seatsZoneRowJoinedTableEntity.SeatsZoneName,
                    SeatsRowId = seatsZoneRowJoinedTableEntity.SeatsRowId,
                    SeatsRowName = seatsZoneRowJoinedTableEntity.SeatsRowName,
                    SeatId = seatsEntityModel.Id,
                    SeatIndex = seatsEntityModel.Index
                }).ToArray();

    private IList<BookedSeatEntityModel> GetBookedSeats(EventEntityModel eventFromDb) =>
        _applicationDbContext.BookedSeats
            .Where(bookedSeatEntityModel => bookedSeatEntityModel.EventId == eventFromDb.Id).ToArray();

    private SeatResult? GetUserBookedSeat(EventEntityModel eventFromDb, Guid userId)
    {
        var bookedSeatId = _applicationDbContext.BookedSeats.FirstOrDefault(bookedSeatEntityModel =>
            bookedSeatEntityModel.EventId == eventFromDb.Id && bookedSeatEntityModel.UserId == userId)?.SeatId;

        if (bookedSeatId == null)
            return null;

        var bookedSeat = _applicationDbContext.SeatsZones
            .Join(
                _applicationDbContext.SeatsRows,
                seatsZoneEntityModel => seatsZoneEntityModel.Id,
                seatsRowEntityModel => seatsRowEntityModel.SeatsZoneId,
                (seatsZoneEntityModel, seatsRowEntityModel) => new SeatsZonesSeatsRowsJoinedTableEntity()
                {
                    PlaceId = seatsZoneEntityModel.PlaceId,
                    SeatsZoneId = seatsZoneEntityModel.Id,
                    SeatsRowId = seatsRowEntityModel.Id
                })
            .Join(_applicationDbContext.Seats,
                seatsZoneRowJoinedTableEntityModel => seatsZoneRowJoinedTableEntityModel.SeatsRowId,
                seatsEntityModel => seatsEntityModel.SeatsRowId,
                (seatsZoneRowJoinedTableEntity, seatsEntityModel) => new SeatsZonesSeatsRowsSeatsJoinedTableEntity()
                {
                    PlaceId = seatsZoneRowJoinedTableEntity.PlaceId,
                    SeatsZoneId = seatsZoneRowJoinedTableEntity.SeatsZoneId,
                    SeatsRowId = seatsZoneRowJoinedTableEntity.SeatsRowId,
                    SeatId = seatsEntityModel.Id
                })
            .Single(seatsZonesSeatsRowsSeatsJoinedTableEntity => seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatId == bookedSeatId);

        return new SeatResult()
        {
            PlaceId = bookedSeat.PlaceId,
            SeatZoneId = bookedSeat.SeatsZoneId,
            SeatZoneName = bookedSeat.SeatsZoneName,
            SeatRowId = bookedSeat.SeatsRowId,
            SeatRowName = bookedSeat.SeatsRowName,
            SeatId = bookedSeat.SeatId,
            SeatIndex = bookedSeat.SeatIndex
        };
    }

    private int GetTotalSeatsCount(EventEntityModel eventFromDb) =>
        _applicationDbContext.SeatsZones
            .Join(
                _applicationDbContext.SeatsRows,
                seatsZoneEntityModel => seatsZoneEntityModel.Id,
                seatsRowEntityModel => seatsRowEntityModel.SeatsZoneId,
                (seatsZoneEntityModel, seatsRowEntityModel) => new SeatsZonesSeatsRowsJoinedTableEntity()
                {
                    PlaceId = seatsZoneEntityModel.PlaceId,
                    SeatsZoneId = seatsZoneEntityModel.Id,
                    SeatsZoneName = seatsZoneEntityModel.Name,
                    SeatsRowId = seatsRowEntityModel.Id,
                    SeatsRowName = seatsRowEntityModel.Name
                })
            .Join(_applicationDbContext.Seats,
                seatsZoneRowJoinedTableEntityModel => seatsZoneRowJoinedTableEntityModel.SeatsRowId,
                seatsEntityModel => seatsEntityModel.SeatsRowId,
                (seatsZoneRowJoinedTableEntity, seatsEntityModel) => new SeatsZonesSeatsRowsSeatsJoinedTableEntity()
                {
                    PlaceId = seatsZoneRowJoinedTableEntity.PlaceId,
                    SeatsZoneId = seatsZoneRowJoinedTableEntity.SeatsZoneId,
                    SeatsZoneName = seatsZoneRowJoinedTableEntity.SeatsZoneName,
                    SeatsRowId = seatsZoneRowJoinedTableEntity.SeatsRowId,
                    SeatsRowName = seatsZoneRowJoinedTableEntity.SeatsRowName,
                    SeatId = seatsEntityModel.Id,
                    SeatIndex = seatsEntityModel.Index
                })
            .Count(seatsZoneRowSeatJoinedTableEntityModel => seatsZoneRowSeatJoinedTableEntityModel.PlaceId == eventFromDb.PlaceId);

    private List<SeatResult> GetAvailableSeats(EventEntityModel eventFromDb)
    {
        var totalSeats = GetTotalSeats(eventFromDb);
        var bookedSeatsIds = GetBookedSeats(eventFromDb).Select(bookedSeat => bookedSeat.SeatId);
        return totalSeats
            .Where(seatsZonesSeatsRowsSeatsJoinedTableEntity => !bookedSeatsIds.Contains(seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatId))
            .Select(seatsZonesSeatsRowsSeatsJoinedTableEntity => new SeatResult()
            {
                PlaceId = seatsZonesSeatsRowsSeatsJoinedTableEntity.PlaceId,
                SeatZoneId = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatsZoneId,
                SeatZoneName = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatsZoneName,
                SeatRowId = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatsRowId,
                SeatRowName = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatsRowName,
                SeatId = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatId,
                SeatIndex = seatsZonesSeatsRowsSeatsJoinedTableEntity.SeatIndex
            })
            .ToList();
    }

    #endregion METHODS

}