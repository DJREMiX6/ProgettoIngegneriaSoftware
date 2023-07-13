﻿using Microsoft.Extensions.Logging;
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

    public Task<IList<EventResult>> GetEvents(Guid userId) =>
        Task.Run(() =>
        {
            var eventsFromDb = _applicationDbContext.Events.ToList();
            IList<EventResult> events = new List<EventResult>();
            foreach (var eventFromDb in eventsFromDb)
                events.Add(CreateEventResult(eventFromDb, userId));

            return events;
        });

    public Task<EventResult?> GetEvent(Guid eventId, Guid userId) =>
        Task.Run(() =>
        {
            var eventFromDb = _applicationDbContext.Events.Find(eventId);
            return eventFromDb == null ? null : CreateEventResult(eventFromDb, userId);
        });

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

    private EventResult CreateEventResult(EventEntityModel eventEntityModel, Guid userId)
    {
        var placeName = GetPlaceEntityModel(eventEntityModel.PlaceId).Name;
        var totalSeatsCount = GetTotalSeatsCount(eventEntityModel);
        var availableSeats = GetAvailableSeats(eventEntityModel);
        var bookedSeat = GetUserBookedSeat(eventEntityModel, userId);
        return new EventResult()
        {
            Id = eventEntityModel.Id,
            Name = eventEntityModel.Name,
            Description = eventEntityModel.Description,
            Date = eventEntityModel.Date,
            Location = placeName,
            ImageSource = eventEntityModel.ImageSrc,
            TotalSeatsCount = totalSeatsCount,
            AvailableSeats = availableSeats,
            BookedSeat = bookedSeat
        };
    }

    #endregion METHODS

}