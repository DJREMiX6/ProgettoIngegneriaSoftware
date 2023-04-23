namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Abstraction
{
    public interface IReadableEventModel
    {
        Guid Id { get; }
        Guid CreatorId { get; }
        string Name { get; }
        string Description { get; }
        DateTime Date { get; }
        string Location { get; }
        string ImageSource { get; }
        int Seats { get; }
        int BookedSeats { get; }
        bool IsBookedByCurrentUser { get; }
    }
}
