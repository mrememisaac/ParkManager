using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class DriversRepository : BaseRepository<Driver>, IDriversRepository
    {
        public DriversRepository(ParkMangerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
