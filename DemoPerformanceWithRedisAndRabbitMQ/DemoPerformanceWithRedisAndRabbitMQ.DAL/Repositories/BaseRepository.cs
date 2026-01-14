using DemoPerformanceWithRedisAndRabbitMQ.DAL.Data;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories
{
    /// <summary>
    ///  Information about the base repository.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="E"></typeparam>
    public class BaseRepository<K, E> : IBaseRepository<K, E> where E : class
    {
        private readonly AppDbContext _dbContext;
        protected readonly DbSet<E> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<E>();
        }

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<E>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<E?> GetByIdAsync(K id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(E entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task DeleteAsync(E entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(E entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
    }
}
