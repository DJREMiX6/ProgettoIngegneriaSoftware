using ProgettoIngegneriaSoftware.Shared.Library.Models.Abstraction;

namespace ProgettoIngegneriaSoftware.UI.Models;

public class SeatResult : ISeatResult
{

    #region PROPS

    public Guid PlaceId { get; set; }
    public Guid SeatZoneId { get; set; }
    public string SeatZoneName { get; set; }
    public Guid SeatRowId { get; set; }
    public string SeatRowName { get; set; }
    public Guid SeatId { get; set; }
    public int SeatIndex { get; set; }

    #endregion PROPS

}