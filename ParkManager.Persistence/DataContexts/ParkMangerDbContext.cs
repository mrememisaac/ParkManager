using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Contracts.Authentication;
using ParkManager.Domain;

namespace ParkManager.Persistence.DataContexts
{
    public class ParkManagerDbContext : DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;

        public ParkManagerDbContext(DbContextOptions<ParkManagerDbContext> options, ILoggedInUserService? loggedInUserService) : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Park> Parks { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Occasion> Occasions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParkManagerDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
