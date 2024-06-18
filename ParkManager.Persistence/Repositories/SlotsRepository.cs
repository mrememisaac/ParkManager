using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class SlotsRepository : BaseRepository<Slot>, ISlotsRepository
    {
        public SlotsRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<SlotsRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
