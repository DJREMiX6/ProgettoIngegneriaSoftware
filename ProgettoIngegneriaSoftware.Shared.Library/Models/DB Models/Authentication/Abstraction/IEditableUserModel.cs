namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Authentication.Abstraction
{
    public interface IEditableUserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
