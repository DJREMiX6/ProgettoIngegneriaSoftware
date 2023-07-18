namespace ProgettoIngegneriaSoftware.API.Services.EventService.Exceptions;

public class SeatUnavailableException : Exception
{

    private const string SEAT_ALREADY_BOOKED_EXCEPTION_MESSAGE = "The seat {0} is unavailable.";

    public SeatUnavailableException(Guid seatId) : base(string.Format(SEAT_ALREADY_BOOKED_EXCEPTION_MESSAGE, seatId)) {}
}