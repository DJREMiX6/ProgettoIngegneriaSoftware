using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.Attributes.DataAnnotation;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Abstraction;
using ProgettoIngegneriaSoftware.Utils.Consts;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records
{
    public record UserModelRecord : IEditableUserModel
    {

        private string _password;
        private string _confirmPassword;

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: IUserModel.USERNAME_MAX_LENGTH, MinimumLength = IUserModel.USERNAME_MIN_LENGTH)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [StringLength(IUserModel.EMAIL_MAX_LENGTH, MinimumLength = IUserModel.EMAIL_MIN_LENGTH)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Password]
        [MinLength(PasswordConsts.MINIMUM_PASSWORD_LENGTH)]
        [MaxLength(PasswordConsts.MAXIMUM_PASSWORD_LENGTH)]
        public string Password
        {
            get => _password;
            set => _password = value.Trim();
        }

        [Password]
        [Required(AllowEmptyStrings = false)]
        [MinLength(PasswordConsts.MINIMUM_PASSWORD_LENGTH)]
        [MaxLength(PasswordConsts.MAXIMUM_PASSWORD_LENGTH)]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = value.Trim();
        }
    }
}
