using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class SeatEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public int Index { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATION PROPERTIES

    [Required]
    [ForeignKey(nameof(Row))]
    public Guid SeatsRowId { get; set; }
    public SeatsRowEntityModel Row { get; set; }

    public List<BookedSeatEntityModel> BookedSeats { get; } = new();

    #endregion NAVIGATION PROPERTIES

}