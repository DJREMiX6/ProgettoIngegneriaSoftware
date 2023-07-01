using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatsRowEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATION PROPERTIES

    [Required]
    [ForeignKey(nameof(SeatsZone))]
    public Guid SeatsZoneId { get; set; }
    public SeatsZoneEntityModel SeatsZone { get; init; }

    public List<SeatEntityModel> Seats { get; } = new();

    #endregion NAVIGATION PROPERTIES

}