using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class ParksRepository : BaseRepository<Park>, IParksRepository
    {
        public ParksRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<ParksRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
