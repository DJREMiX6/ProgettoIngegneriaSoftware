using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.API.Attributes.DataAnnotation;
using ProgettoIngegneriaSoftware.API.Constants;

namespace ProgettoIngegneriaSoftware.API.Models;

public class RegisterUserInfo
{

    #region PROPERTIES

    [Required(AllowEmptyStrings = false)]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_USERNAME_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_USERNAME_MIN_LENGTH)]
    public string UserName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_EMAIL_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_EMAIL_MIN_LENGTH)]
    public string Email { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Password]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_PASSWORD_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_PASSWORD_MIN_LENGTH)]
    public string Password { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Password]
    [StringLength(maximumLength: ModelsConsts.USER_INFO_PASSWORD_MAX_LENGTH, MinimumLength = ModelsConsts.USER_INFO_PASSWORD_MIN_LENGTH)]
    public string ConfirmPassword { get; set; } = string.Empty;

    #endregion PROPERTIES

}