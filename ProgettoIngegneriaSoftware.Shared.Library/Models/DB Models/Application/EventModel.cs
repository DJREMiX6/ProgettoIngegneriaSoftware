using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProgettoIngegneriaSoftware.Shared.Library.Attributes.DataAnnotation;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Abstraction;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;

public class EventModel : IEventModel
{

    #region CTORS

    public EventModel() { }

    public EventModel(IEditableEventModel editableEventModel)
    {
        Name = editableEventModel.Name;
        Description = editableEventModel.Description;
        Date = editableEventModel.Date;
        Seats = editableEventModel.Seats;
    }

    #endregion CTORS

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid CreatorId { get; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(IEventModel.EVENT_NAME_MAX_LENGTH, MinimumLength = IEventModel.EVENT_NAME_MIN_LENGTH)]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(IEventModel.EVENT_DESCRIPTION_MAX_LENGTH, MinimumLength = IEventModel.EVENT_DESCRIPTION_MIN_LENGTH)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    public string Location { get; set; }

    [Required]
    public int Seats { get; set; }

    public string ImageSource { get; set; }

    [NotMapped]
    public int BookedSeats { get; }

    [NotMapped]
    public bool IsBookedByCurrentUser { get; }
}