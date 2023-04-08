using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public class EventsService : IEventsService
{

    #region FIELDS

    private HttpClient _httpClient;

    #endregion FIELDS

    #region CTORS

    public EventsService()
    {
        _httpClient = new HttpClient();
    }

    #endregion CTORS

    #region IEventsService IMPLEMENTATION

    public async Task<List<EventModel>> GetEventsAsync()
    {
        var events = new List<EventModel>();
        events.Add(new EventModel()
        {
            Id = new Guid(),
            Date = new DateTime(2023, 8, 21),
            Description = "The best of all time.",
            Name = "Nanowar Of Steel",
            Seats = 20000,
            ImageSource = "https://www.angrymetalguy.com/wp-content/uploads/2018/11/Nanowar-of-Steel-Stairway-to-Valhalla-01-500x497.jpg"
        });
        events.Add(new EventModel()
        {
            Id = new Guid(),
            Date = new DateTime(2023, 8, 21),
            Description = "The best of all time.",
            Name = "Nanowar Of Steel",
            Seats = 20000,
            ImageSource = "https://www.angrymetalguy.com/wp-content/uploads/2018/11/Nanowar-of-Steel-Stairway-to-Valhalla-01-500x497.jpg"
        });
        events.Add(new EventModel()
        {
            Id = new Guid(),
            Date = new DateTime(2023, 8, 21),
            Description = "The best of all time.",
            Name = "Nanowar Of Steel",
            Seats = 20000,
            ImageSource = "https://www.angrymetalguy.com/wp-content/uploads/2018/11/Nanowar-of-Steel-Stairway-to-Valhalla-01-500x497.jpg"
        });
        events.Add(new EventModel()
        {
            Id = new Guid(),
            Date = new DateTime(2023, 8, 21),
            Description = "The best of all time.",
            Name = "Nanowar Of Steel",
            Seats = 20000,
            ImageSource = "https://www.angrymetalguy.com/wp-content/uploads/2018/11/Nanowar-of-Steel-Stairway-to-Valhalla-01-500x497.jpg"
        });

        return events;
    }

    #endregion IEventsService IMPLEMENTATION

}