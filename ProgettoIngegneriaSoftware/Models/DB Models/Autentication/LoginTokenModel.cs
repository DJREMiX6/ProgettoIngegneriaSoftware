using System.ComponentModel.DataAnnotations;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Autentication
{
    public class LoginTokenModel
    {

        public const int TOKEN_LENGTH = 255;

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        [StringLength(TOKEN_LENGTH)]
        public string Token { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsExpired { get; set; }

    }
}
