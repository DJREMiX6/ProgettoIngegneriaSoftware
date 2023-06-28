using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.API.Models.DB;

namespace ProgettoIngegneriaSoftware.API;

public class ApplicationDbContext : DbContext
{

    #region CTORS

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    #endregion CTORS

    #region DBSETS

    public DbSet<UserEntityModel> Users { get; set; }

    #endregion DBSETS

}