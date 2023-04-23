using ProgettoIngegneriaSoftware.UI.Models;

namespace ProgettoIngegneriaSoftware.UI.Services.EventsService;

public class EventsServiceLocal : IEventsService
{

    #region FIELDS

    private List<ReadableEventModel> _events;

    #endregion FIELDS

    #region CTORS

    public EventsServiceLocal()
    {
        SetUpEvents();
    }

    #endregion CTORS

    #region IEventsService IMPLEMENTATION

    public async Task<List<ReadableEventModel>> GetEventsAsync() => _events;

    public async Task<bool> FollowEventAsync(int eventId)
    {
        var eventModel = _events.FirstOrDefault(eventModel => eventModel.Id == eventId);
        if (eventModel == null) return false;
        if (!eventModel.IsBookable) return false;
        if (eventModel.IsBookedByCurrentUser) return false;

        eventModel.IsBookedByCurrentUser = true;
        eventModel.BookedSeats++;
        return true;
    }

    public async Task<bool> UnFollowEventAsync(int eventId)
    {
        var eventModel = _events.FirstOrDefault(eventModel => eventModel.Id == eventId);
        if (eventModel == null) return false;
        if (!eventModel.IsBookedByCurrentUser) return false;

        eventModel.IsBookedByCurrentUser = false;
        eventModel.BookedSeats--;
        return true;
    }

    public async Task<ReadableEventModel?> GetEventAsync(int eventId) =>
        _events.FirstOrDefault(eventModel => eventModel.Id == eventId);

    #endregion IEventsService IMPLEMENTATION

    #region PRIVATE METHODS

    private void SetUpEvents()
    {
        _events = new ()
        {
            new ReadableEventModel()
            {
                Id = 1,
                Date = new DateTime(2023, 8, 21),
                Description = "Nanowar of Steel (stylised as NanowaR of Steel by the band) is a comedy Italian heavy metal band. Their name is a pun on the metal bands Manowar and Rhapsody of Fire, and represents their tendency to satirize &quot;true metal&quot;, the primary focus of their music. Their work mainly aims to make humorous references to and jokes about the genre, often parodying the way in which power metal bands are perceived to take themselves very seriously.\nIn 2006 the band changed their name from Nanowar to Nanowar of Steel,[1] as a parody of the Italian power metal band Rhapsody changing their name to Rhapsody of Fire after a legal dispute.[2]",
                Name = "Nanowar Of Steel Il Tour",
                Location = "Idroscalo, Milano (MI)",
                Seats = 20000,
                ImageSource = "https://www.angrymetalguy.com/wp-content/uploads/2018/11/Nanowar-of-Steel-Stairway-to-Valhalla-01-500x497.jpg",
                BookedSeats = 19000,
                IsBookedByCurrentUser = false
            },
            new ReadableEventModel()
            {
                Id = 2,
                Date = new DateTime(2023, 8, 21),
                Description = "The Eras Tour è il sesto tour musicale della cantautrice statunitense Taylor Swift che ha tenuto durante il 2023.\r\n\r\nLa tournée celebra la carriera della cantante a diciassette anni dal suo debutto, con una scaletta atta a comprendere tutti gli album pubblicati da lei pubblicati, passando di fatto da Taylor Swift fino a Midnights.",
                Name = "Taylor Swift Eras Tour",
                Location = "Nissan Stadium, Nashville (TN)",
                Seats = 20000,
                ImageSource = "https://s1.ticketm.net/dam/a/477/955a0c60-463c-442a-a3bb-d10aa68dc477_RETINA_PORTRAIT_16_9.jpg",
                BookedSeats = 19999,
                IsBookedByCurrentUser = true
            },
            new ReadableEventModel()
            {
                Id = 3,
                Date = new DateTime(2023, 6, 5),
                Description = "Irish Speedfolk! With this brand of music Fiddler’s Green has gained a reputation as one of the best live acts in Germany. Since 1990 the band has played about 2,000 concerts both at home and abroad, and their renown as live performers is backed up by 14 studio albums, an EP, five live CDs and four DVDs.",
                Name = "Fiddler's Green Acoustic Pub Crawl 2023",
                Location = "Scala, Heidelberg (DE)",
                Seats = 20000,
                ImageSource = "https://www.fiddlers.de/files/fiddlers/img/fg_hd_steet_front_1920.jpg",
                BookedSeats = 19999,
                IsBookedByCurrentUser = false
            },
            new ReadableEventModel()
            {
                Id = 4,
                Date = new DateTime(2023, 8, 21),
                Description = "FAUN are one of the world's leading bands for the fusion of old sounds with modern music and have now released 11 studio CDs and 2 DVDs. Their CD releases reach top positions in the German album charts (eg: \"Pagan\" 3rd place, \"Midgard\" 3rd place, \"Luna\" 4th place). They have also been nominated three times for the Echo, the largest German music award, and achieved platinum status with their CD “Von den Elben” and gold status with their CD “Luna”.",
                Name = "Pagan Tour 2023",
                Location = "Theater am Aegi, Hannover (DE)",
                Seats = 20000,
                ImageSource = "https://faune.de/wp-content/uploads/2022/01/Motiv-Faun-Pagan-Vinyl-Innen-Kopie-min.jpg",
                BookedSeats = 20000,
                IsBookedByCurrentUser = false
            }
        };
    }

    #endregion PRIVATE METHODS

}