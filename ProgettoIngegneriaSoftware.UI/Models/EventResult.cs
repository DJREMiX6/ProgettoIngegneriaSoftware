using System.Text.Json.Serialization;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Services.ApiHttpClient;

namespace ProgettoIngegneriaSoftware.UI.Models;

public class EventResult : IDisplayEvent
{

    #region PROPS

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string ImageSource { get; set; }
    public int TotalSeatsCount { get; set; }
    public ICollection<ISeatResult> AvailableSeats { get; set; }
    public ICollection<ISeatResult> BookedSeats { get; set; }
    public bool IsBookedByCurrentUser => BookedSeats.Count > 0;
    public bool IsFull => AvailableSeats.Count == 0;
    public string FormattedSeats => $"{TotalSeatsCount - BookedSeats.Count}/{TotalSeatsCount}";

    #endregion PROPS

}
