using System.ComponentModel.DataAnnotations;
using ProgettoIngegneriaSoftware.Shared.Library.Utils.Consts;

namespace ProgettoIngegneriaSoftware.Shared.Library.Attributes.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class PasswordAttribute : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            EnsureLegalValue(value);

            var password = ((string)value).Trim();

            if (password.Length < PasswordConsts.MINIMUM_PASSWORD_LENGTH)
            {
                return false;
            }

            if (password.Length > PasswordConsts.MAXIMUM_PASSWORD_LENGTH)
            {
                return false;
            }
            if (password.Where(c => char.IsSymbol(c) || char.IsPunctuation(c)).Count() == 0)
            {

            }
            if (password.Where(c => char.IsDigit(c)).Count() == 0)
            {
                return false;
            }
            if (password.Where(c => char.IsUpper(c)).Count() == 0)
            {
                return false;
            }
            if (password.Where(c => char.IsLower(c)).Count() == 0)
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
