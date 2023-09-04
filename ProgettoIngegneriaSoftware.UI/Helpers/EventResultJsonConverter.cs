using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;
using ProgettoIngegneriaSoftware.UI.Models;

namespace ProgettoIngegneriaSoftware.UI.Helpers
{
    public class EventResultJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            return new EventResult()
            {
                Id = jObject[nameof(IEventResult.Id)].ToObject<Guid>(),
                Name = jObject[nameof(IEventResult.Name)].ToObject<string>(),
                Description = jObject[nameof(IEventResult.Description)].ToObject<string>(),
                Date = jObject[nameof(IEventResult.Date)].ToObject<DateTime>(),
                Location = jObject[nameof(IEventResult.Location)].ToObject<string>(),
                ImageSource = jObject[nameof(IEventResult.ImageSource)].ToObject<string>(),
                TotalSeatsCount = jObject[nameof(IEventResult.TotalSeatsCount)].ToObject<int>(),
                AvailableSeats = jObject[nameof(IEventResult.AvailableSeats)].ToObject<SeatResult[]>() ?? Array.Empty<SeatResult>(),
                BookedSeats = jObject[nameof(IEventResult.BookedSeats)].ToObject<SeatResult[]>() ?? Array.Empty<SeatResult>()
            };
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(IEventResult);
    }
}
