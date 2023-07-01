using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.API.Constants;

namespace ProgettoIngegneriaSoftware.API.Models.DB;

public class UserEntityModel
{

    #region ENTITY ATTRIBUTES

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_USERNAME_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_USERNAME_MIN_LENGTH)]
    public string Username { get; set; }

    [EmailAddress]
    [Required(AllowEmptyStrings = false)]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_EMAIL_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_EMAIL_MIN_LENGTH)]
    public string Email { get; set; }

    [Required]
    [MinLength(ModelsConsts.USER_INFO_PASSWORD_HASH_LENGTH)]
    [MaxLength(ModelsConsts.USER_INFO_PASSWORD_HASH_LENGTH)]
    public byte[] PasswordHash { get; set; }

    [Required]
    [MinLength(ModelsConsts.USER_INFO_PASSWORD_SALT_LENGTH)]
    [MaxLength(ModelsConsts.USER_INFO_PASSWORD_SALT_LENGTH)]
    public byte[] PasswordSalt { get; set; }

    #endregion ENTITY ATTRIBUTES

    #region NAVIGATION PROPERTIES

    public List<BookedSeatEntityModel> BookedSeats { get; } = new();

    #endregion NAVIGATION PROPERTIES

}