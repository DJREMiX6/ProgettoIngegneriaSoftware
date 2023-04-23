namespace ProgettoIngegneriaSoftware.UI.Models;

public class ReadableEventModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string ImageSource { get; set; }
    public int Seats { get; set; }
    public int BookedSeats { get; set; }
    public bool IsBookedByCurrentUser { get; set; }
    public bool IsFull => BookedSeats >= Seats;
    public bool IsBookable => !IsFull && !IsBookedByCurrentUser;
    public string DisplaySeats => $"{BookedSeats}/{Seats}";
}