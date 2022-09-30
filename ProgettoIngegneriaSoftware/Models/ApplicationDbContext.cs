using Microsoft.EntityFrameworkCore;

namespace ProgettoIngegneriaSoftware.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //TODO
        // PUBLIC MODELS EX: public DbSet<ExampleModelName> Examples { get; set; } 

    }
}
