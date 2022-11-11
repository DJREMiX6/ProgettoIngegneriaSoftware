namespace ProgettoIngegneriaSoftware.Models.DB_Models.Application.Abstraction
{
    public interface IReadableEventModel
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        DateTime Date { get; }
        int Seats { get; }
        int BookedSeats { get; }
    }
}
