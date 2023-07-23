using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Models.Abstraction;

public interface IDisplayEvent : IEventResult
{
    public bool IsBookedByCurrentUser { get; }
    public bool IsFull { get; }
    public string FormattedSeats { get; }
}