using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class LanesRepository : BaseRepository<Lane>, ILanesRepository
    {
        public LanesRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
