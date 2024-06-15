using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class SlotsRepository : BaseRepository<Slot>, ISlotsRepository
    {
        public SlotsRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
