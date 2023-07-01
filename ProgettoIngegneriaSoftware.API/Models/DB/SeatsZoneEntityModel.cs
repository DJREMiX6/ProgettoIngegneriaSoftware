using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatsZoneEntityModel
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
    [ForeignKey(nameof(Place))]
    public Guid PlaceId { get; set; }
    public PlaceEntityModel Place { get; init; }

    public List<SeatsRowEntityModel> SeatsRows { get; } = new();

    #endregion NAVIGATION PROPERTIES

}