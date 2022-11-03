using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Authentication
{
    public class LoginTokenModel
    {

        public const int TOKEN_LENGTH = 255;

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }

        [Required]
        public Guid UserId { get; set; }

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
