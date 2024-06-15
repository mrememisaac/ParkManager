using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class OccasionsRepository : BaseRepository<Occasion>, IOccasionsRepository
    {
        public OccasionsRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
