using Microsoft.EntityFrameworkCore;
using ParkManager.Domain;

namespace ParkManager.Persistence.DataContexts
{
    public class ParkMangerDbContext : DbContext
    {
        public ParkMangerDbContext(DbContextOptions<ParkMangerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Park> Parks { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
