using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class ArrivalsRepository : BaseRepository<Arrival>, IArrivalsRepository
    {

        public ArrivalsRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<ArrivalsRepository> logger) : base(dbContext, cache, logger)
        {
            
        }
    }
}
