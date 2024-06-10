namespace ParkManager.Application.Contracts.Persistence
{
    public interface IRepository<Entity>
    {
        Task<Entity> Add(Entity entity);
        Task<Entity> Update(Entity entity);
        Task<Entity> Get(int id);
        Task Delete(int id);
    }
}
