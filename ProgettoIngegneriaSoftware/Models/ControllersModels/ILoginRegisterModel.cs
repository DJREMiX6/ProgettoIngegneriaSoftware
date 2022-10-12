using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;

namespace ProgettoIngegneriaSoftware.Models.ControllersModels
{
    public interface ILoginRegisterModel
    {
        public bool IsPasswordValid(string password, string confirmPassword);

        public Task<bool> IsPasswordValid(string password, UserModel user);

        public Task<bool> IsLoginTokenValid(string loginToken, string username);

        public Task<bool> IsLoginTokenValid(string loginToken, UserModel user);

        public Task<bool> LoginTokenExists(string loginToken);

        public Task<bool> UserExistsByUsername(string username);

        public Task<bool> UserExistsByEmail(string email);

        public Task<bool> CreateNewUser(string username, string email, string password);

        public Task<UserModel?> GetUserByUsername(string username);

        public Task<UserModel?> GetUserByEmail(string email);

        public Task<string> GenerateToken(UserModel user);

        public Task<bool> ExpireLoginToken(string loginToken);
    }
}
