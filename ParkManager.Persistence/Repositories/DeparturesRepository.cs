using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class DeparturesRepository : BaseRepository<Departure>, IDeparturesRepository
    {
        public DeparturesRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<DeparturesRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
