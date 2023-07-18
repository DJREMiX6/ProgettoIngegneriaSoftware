namespace ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

public interface IEventResult
{

    #region PROPERTIES

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string ImageSource { get; set; }
    public int TotalSeatsCount { get; set; }
    public ICollection<ISeatResult> AvailableSeats { get; set; }
    public ICollection<ISeatResult> BookedSeats { get; set; }

    #endregion PROPERTIES

}