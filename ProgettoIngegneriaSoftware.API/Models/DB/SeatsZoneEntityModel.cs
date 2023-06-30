namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatsZoneEntityModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PlaceEntityModel Place { get; set; }
}