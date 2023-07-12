namespace ProgettoIngegneriaSoftware.API.Models.HelperModels;

public class SeatsZonesSeatsRowsSeatsJoinedTableEntity
{
    public Guid PlaceId { get; set; }
    public Guid SeatsZoneId { get; set; }
    public string SeatsZoneName { get; set; }
    public Guid SeatsRowId { get; set; }
    public string SeatsRowName { get; set; }
    public Guid SeatId { get; set; }
    public int SeatIndex { get; set; }
}