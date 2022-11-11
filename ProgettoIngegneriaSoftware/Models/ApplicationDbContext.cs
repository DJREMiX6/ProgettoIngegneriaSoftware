using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application;

namespace ProgettoIngegneriaSoftware.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EventModel> Events { get; set; }

    }
}
