using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class ParksRepository : BaseRepository<Park>, IParksRepository
    {
        public ParksRepository(ParkMangerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
