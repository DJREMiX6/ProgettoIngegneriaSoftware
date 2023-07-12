namespace ProgettoIngegneriaSoftware.API.Models.HelperModels;

public class SeatsZonesSeatsRowsJoinedTableEntity
{
    public Guid PlaceId { get; set; }
    public Guid SeatsZoneId { get; set; }
    public string SeatsZoneName { get; set; }
    public Guid SeatsRowId { get; set; }
    public string SeatsRowName { get; set; }
}