using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class OccasionsRepository : BaseRepository<Occasion>, IOccasionsRepository
    {
        public OccasionsRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<OccasionsRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
