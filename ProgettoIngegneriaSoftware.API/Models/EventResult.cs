namespace ProgettoIngegneriaSoftware.API.Models;

public class EventResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string ImageSource { get; set; }
    public int TotalSeatsCount { get; set; }
    public IList<SeatResult> AvailableSeats { get; set; }
    public IList<SeatResult> BookedSeats { get; set; }
}