using ProgettoIngegneriaSoftware.Models.DB_Models.Application;

namespace ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Abstraction
{
    public interface IUserModel
    {
        #region PUBLIC CONSTS

        public const int USERNAME_MAX_LENGTH = 32;
        public const int USERNAME_MIN_LENGTH = 3;
        public const int EMAIL_MAX_LENGTH = 128;
        public const int EMAIL_MIN_LENGTH = 6;
        public const int PASSWORD_HASH_LENGTH = 128;
        public const int SALT_LENGTH = 32;

        #endregion PUBLIC CONSTS

        #region MODEL ATTRIBUTES

        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string Email { get; set; }
        public long ConfirmationToken { get; set; }
        public ICollection<EventModel> AdministratedEvents { get; set; }
        public ICollection<EventModel> EventsSubscribed { get; set; }

        #endregion MODEL ATTRIBUTES
    }
}
