using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class DeparturesRepository : BaseRepository<Departure>, IDeparturesRepository
    {
        public DeparturesRepository(ParkMangerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
