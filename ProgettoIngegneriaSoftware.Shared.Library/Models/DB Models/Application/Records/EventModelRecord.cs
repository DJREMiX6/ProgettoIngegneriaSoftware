using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Abstraction;

namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Records
{
    public record EventModelRecord : IEditableEventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Seats { get; set; }
    }
}
