using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Models;

class ReadableEventModel : IReadableEventModel
{
    public Guid Id { get; }
    public Guid CreatorId { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime Date { get; }
    public string ImageSource { get; }
    public int Seats { get; }
    public int BookedSeats { get; }
    public bool IsBooked { get; }
    public bool IsBookable => BookedSeats < Seats && !IsBooked;
}