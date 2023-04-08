using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Authentication.Abstraction;

namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Authentication
{
    public class UserModel : IUserModel, IReadableUseModel
    {

        #region MODEL ATTRIBUTES

        [Key]
        [Required(AllowEmptyStrings = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: IUserModel.USERNAME_MAX_LENGTH, MinimumLength = IUserModel.USERNAME_MIN_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MaxLength(IUserModel.PASSWORD_HASH_LENGTH)]
        [MinLength(IUserModel.PASSWORD_HASH_LENGTH)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(IUserModel.SALT_LENGTH)]
        [MinLength(IUserModel.SALT_LENGTH)]
        public byte[] Salt { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(maximumLength: IUserModel.EMAIL_MAX_LENGTH, MinimumLength = IUserModel.EMAIL_MIN_LENGTH)]
        public string Email { get; set; }

        [Required]
        public long ConfirmationToken { get; set; }

        [Required]
        public ICollection<EventModel> AdministratedEvents { get; set; }

        [Required]
        public ICollection<EventModel> EventsSubscribed { get; set; }

        #endregion MODEL ATTRIBUTES

        #region NOT MAPPED ATTRIBUTES

        [NotMapped] public bool IsConfirmed => ConfirmationToken == 0;

        #endregion NOT MAPPED ATTRIBUTES

    }
}
