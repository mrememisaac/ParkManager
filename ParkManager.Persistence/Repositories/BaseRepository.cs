using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;
using ParkManager.Shared.Caching;

namespace ParkManager.Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly ParkManagerDbContext _dbContext;
        private readonly IDistributedCache _cache;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ParkManagerDbContext dbContext, IDistributedCache cache, ILogger<BaseRepository<T>> logger)
        {
            _dbContext = dbContext;
            _cache = cache;
            _logger = logger;
        }

        public async Task<T> Add(T entity)
        {
            _logger.LogInformation($"Adding entity of type {entity.GetType().Name} to the context.");
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Guid id)
        {
            _logger.LogInformation($"Deleting entity of type {typeof(T).Name} with id {id} from the context.");
            var entity = await Get(id);
            var key = $"{typeof(T).Name}-{id}";
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            await _cache.RemoveAsync(key);
        }

        public async Task<T> Get(Guid id)
        {
            _logger.LogInformation($"Getting entity of type {typeof(T).Name} with id {id} from the context.");

            if (id == Guid.Empty)
            {
                _logger.LogWarning($"Entity of type {typeof(T).Name} with id {id} is not valid.");
                return null;
            }
            var key = $"{typeof(T).Name}-{id}";
            var item = await _cache.GetFromCache<T>(key);
            if (item != null)
            {
                _logger.LogInformation($"Entity of type {typeof(T).Name} with id {id} was found in the cache.");
                return item;
            }
            item = await _dbContext.Set<T>().FindAsync(id);
            if (item != null)
            {
                await _cache.SaveToCache(key, item);
            }
            return item;
        }

        public async Task<T> Update(T entity)
        {
            _logger.LogInformation($"Updating entity of type {entity.GetType().Name} in the context.");
            var item = _dbContext.Set<T>().Update(entity).Entity;
            await _dbContext.SaveChangesAsync();
            var key = $"{typeof(T).Name}-{entity.Id}";
            await _cache.SaveToCache(key, item);
            return entity;
        }

        public async Task<List<T>> List(int count, int page)
        {
            _logger.LogInformation($"Getting {count} entities of type {typeof(T).Name} from page {page}.");
            return await _dbContext.Set<T>().Take(count).Skip(page * count).ToListAsync();
        }
    }
}
