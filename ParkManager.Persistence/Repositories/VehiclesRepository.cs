using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class VehiclesRepository : BaseRepository<Vehicle>, IVehiclesRepository
    {
        public VehiclesRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
