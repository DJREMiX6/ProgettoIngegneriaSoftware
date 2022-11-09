using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Authentication
{
    public class UserModel
    {

        #region PUBLIC CONSTS

        public const int USERNAME_MAX_LENGTH = 32;
        public const int USERNAME_MIN_LENGTH = 3;
        public const int EMAIL_MAX_LENGTH = 128;
        public const int EMAIL_MIN_LENGTH = 6;
        public const int PASSWORD_HASH_LENGTH = 128;
        public const int SALT_LENGTH = 32;

        #endregion PUBLIC CONSTS

        #region MODEL ATTRIBUTES

        [Key]
        [Required(AllowEmptyStrings = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: USERNAME_MAX_LENGTH, MinimumLength = USERNAME_MIN_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MaxLength(PASSWORD_HASH_LENGTH)]
        [MinLength(PASSWORD_HASH_LENGTH)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(SALT_LENGTH)]
        [MinLength(SALT_LENGTH)]
        public byte[] Salt { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(maximumLength: EMAIL_MAX_LENGTH, MinimumLength = EMAIL_MIN_LENGTH)]
        public string Email { get; set; }

        [NotMapped] public bool IsConfirmed => ConfirmationToken == 0;

        [Required]
        public long ConfirmationToken { get; set; }

        #endregion MODEL ATTRIBUTES

    }
}
