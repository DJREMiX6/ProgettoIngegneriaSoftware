using Microsoft.EntityFrameworkCore;

namespace ProgettoIngegneriaSoftware.Models
{
    public class AutenticationDbContext : DbContext
    {
        public AutenticationDbContext(DbContextOptions<AutenticationDbContext> options) : base(options)
        { }


    }
}
