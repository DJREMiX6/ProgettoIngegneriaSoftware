using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class BookedSeatEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATION PROPERTIES

    [Required]
    [ForeignKey(nameof(Seat))]
    public Guid SeatId { get; set; }
    public SeatEntityModel Seat { get; }

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public UserEntityModel User { get; }

    [Required]
    [ForeignKey(nameof(Event))]
    public Guid EventId { get; set; }
    public EventEntityModel Event { get; }

    #endregion NAVIGATION PROPERTIES

}