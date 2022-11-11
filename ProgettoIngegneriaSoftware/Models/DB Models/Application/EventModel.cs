using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using ProgettoIngegneriaSoftware.Attributes.DataAnnotation;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application.Abstraction;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Application
{
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

        [Required(AllowEmptyStrings = false)]
        [StringLength(IEventModel.EVENT_NAME_MAX_LENGTH, MinimumLength = IEventModel.EVENT_NAME_MIN_LENGTH)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(IEventModel.EVENT_DESCRIPTION_MAX_LENGTH, MinimumLength = IEventModel.EVENT_DESCRIPTION_MIN_LENGTH)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public int Seats { get; set; }

        [NotMapped]
        public int BookedSeats { get; }
    }
}
