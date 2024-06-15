using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class ArrivalsRepository : BaseRepository<Arrival>, IArrivalsRepository
    {
        public ArrivalsRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
