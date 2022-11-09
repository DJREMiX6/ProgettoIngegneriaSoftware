using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Models
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }

    }
}
