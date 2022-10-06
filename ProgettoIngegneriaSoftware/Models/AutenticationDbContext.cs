using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models.DB_Models.Autentication;

namespace ProgettoIngegneriaSoftware.Models
{
    public class AutenticationDbContext : DbContext
    {
        public AutenticationDbContext(DbContextOptions<AutenticationDbContext> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<LoginTokenModel> LoginTokens { get; set; }

    }
}
