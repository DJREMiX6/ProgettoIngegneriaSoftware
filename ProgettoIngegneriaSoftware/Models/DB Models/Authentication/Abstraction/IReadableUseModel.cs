namespace ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Abstraction
{
    public interface IReadableUseModel
    {
        public string Username { get; }
        public string Email { get; }
    }
}
