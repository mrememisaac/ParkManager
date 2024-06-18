using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class DriversRepository : BaseRepository<Driver>, IDriversRepository
    {
        public DriversRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<DriversRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
