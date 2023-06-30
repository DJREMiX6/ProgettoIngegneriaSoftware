namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatEntityModel
{
    public Guid Id { get; set; }
    public int Index { get; set; }
    public SeatsRowEntityModel Row { get; set; }
}//TODO ADD DATA ANNOTATIONS TO THE NEW MODELS AND CREATE A MIGRATION WITH RELATIONS