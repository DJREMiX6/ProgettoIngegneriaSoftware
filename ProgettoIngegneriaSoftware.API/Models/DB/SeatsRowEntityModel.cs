namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatsRowEntityModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SeatsZoneEntityModel SeatsZone { get; set; }
}