﻿using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Domain;
using ParkManager.Persistence.DataContexts;

namespace ParkManager.Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly ParkManagerDbContext _dbContext;

        public BaseRepository(ParkManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Guid id)
        {
            var entity = await Get(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> List(int count, int page)
        {
            return await _dbContext.Set<T>().Take(count).Skip(page * count).ToListAsync();
        }
    }
}
