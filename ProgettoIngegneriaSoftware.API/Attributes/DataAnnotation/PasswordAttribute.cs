using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.API.Constants;

namespace ProgettoIngegneriaSoftware.API.Attributes.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class PasswordAttribute : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            EnsureLegalValue(value);

            var password = ((string)value).Trim();

            if (password.Length < ModelsConsts.USER_INFO_PASSWORD_MIN_LENGTH)
            {
                return false;
            }
            if (password.Length > ModelsConsts.USER_INFO_PASSWORD_MAX_LENGTH)
            {
                return false;
            }
            if (!password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
            {
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            return true;

        }

        private void EnsureLegalValue(object? value)
        {
            if (value is null)
            {
                throw new ArgumentNullException($"The {nameof(value)} is null.");
            }
            if (value.GetType() != typeof(string))
            {
                throw new InvalidCastException($"The {nameof(value)} must be of type {typeof(string)}.");
            }
        }
    }
}
