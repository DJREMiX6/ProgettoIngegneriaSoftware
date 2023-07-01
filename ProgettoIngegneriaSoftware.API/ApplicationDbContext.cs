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
    public DbSet<PlaceEntityModel> Places { get; set; }
    public DbSet<EventEntityModel> Events { get; set; }
    public DbSet<SeatsZoneEntityModel> SeatsZones { get; set; }
    public DbSet<SeatsRowEntityModel> SeatsRows { get; set; }
    public DbSet<SeatEntityModel> Seats { get; set; }
    public DbSet<BookedSeatEntityModel> BookedSeats { get; set; }

    #endregion DBSETS

}