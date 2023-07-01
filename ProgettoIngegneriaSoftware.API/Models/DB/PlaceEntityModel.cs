using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class PlaceEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Location { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATIONS PROPERTIES

    public List<SeatsZoneEntityModel> SeatsZones { get; } = new();

    #endregion NAVIGATION PROPERTIES

}