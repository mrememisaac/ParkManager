using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class VehiclesRepository : BaseRepository<Vehicle>, IVehiclesRepository
    {
        public VehiclesRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<VehiclesRepository> logger) : base(dbContext, cache, logger)
        {
        }
    }
}
