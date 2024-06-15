using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class TagsRepository : BaseRepository<Tag>, ITagsRepository
    {
        public TagsRepository(ParkManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
