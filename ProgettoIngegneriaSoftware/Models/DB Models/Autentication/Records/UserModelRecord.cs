using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.Attributes.DataAnnotation;
using ProgettoIngegneriaSoftware.Models.ControllersModels;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Autentication.Records
{
    public record UserModelRecord
    {

        private string _password;
        private string _confirmPassword;

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: UserModel.USERNAME_MAX_LENGTH, MinimumLength = UserModel.USERNAME_MIN_LENGTH)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [StringLength(UserModel.EMAIL_MAX_LENGTH, MinimumLength = UserModel.EMAIL_MIN_LENGTH)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Password]
        [MinLength(LoginRegisterModel.MINIMUM_PASSWORD_LENGTH)]
        [MaxLength(LoginRegisterModel.MAXIMUM_PASSWORD_LENGTH)]
        public string Password
        {
            get => _password;
            set => _password = value.Trim();
        }

        [Password]
        [Required(AllowEmptyStrings = false)]
        [MinLength(LoginRegisterModel.MINIMUM_PASSWORD_LENGTH)]
        [MaxLength(LoginRegisterModel.MAXIMUM_PASSWORD_LENGTH)]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = value.Trim();
        }

        public bool IsValidPassword => Password.Equals(ConfirmPassword);
    }
}
