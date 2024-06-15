namespace ParkManager.Application.Contracts.Persistence
{
    public interface IRepository<Entity>
    {
        Task<Entity> Add(Entity entity);
        Task<Entity> Update(Entity entity);
        Task<Entity> Get(Guid id);
        Task Delete(Guid id);
        Task<List<Entity>> List(int count, int page);

    }
}
