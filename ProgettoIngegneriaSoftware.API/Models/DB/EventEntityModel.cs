using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class EventEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string ImageSrc { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATION PROPERTIES

    [Required]
    [ForeignKey(nameof(Place))]
    public Guid PlaceId { get; set; }
    public PlaceEntityModel Place { get; init; }

    public List<BookedSeatEntityModel> BookedSeats { get; } = new();

    #endregion NAVIGATION PROPERTIES

}