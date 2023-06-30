namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class BookedSeatEntityModel
{
    public int Id { get; set; }
    public SeatEntityModel Seat { get; set; }
    public UserEntityModel User { get; set; }
    public EventEntityModel Event { get; set; }
}