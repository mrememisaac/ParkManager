using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class LanesRepository : BaseRepository<Lane>, ILanesRepository
    {
        public LanesRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<LanesRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
