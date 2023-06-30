namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class EventEntityModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public PlaceEntityModel Location { get; set; }
    public string ImageSrc { get; set; }
}