using System.ComponentModel.DataAnnotations;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Autentication
{
    public class UserModel
    {

        public const int USERNAME_LENGTH = 32;
        public const int PASSWORD_HASH_LENGTH = 128;
        public const int SALT_LENGTH = 32;
        public const int EMAIL_LENGTH = 128;

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(USERNAME_LENGTH)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(PASSWORD_HASH_LENGTH)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(SALT_LENGTH)]
        public byte[] Salt { get; set; }

        [Required]
        [StringLength(EMAIL_LENGTH)]
        public string Email { get; set; }

    }
}
