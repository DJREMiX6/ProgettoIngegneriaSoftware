using System.ComponentModel.DataAnnotations;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Autentication
{
    public class LoginTokenModel
    {

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        [StringLength(255)]
        public string Token { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

    }
}
