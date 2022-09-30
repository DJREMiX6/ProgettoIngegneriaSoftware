using System.ComponentModel.DataAnnotations;

namespace ProgettoIngegneriaSoftware.Models.DB_Models
{
    public class UserModel
    {

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] StoredPassword { get; set; }

        [Required]
        [MaxLength(32)]
        public byte[] StoredSalt { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

    }
}
