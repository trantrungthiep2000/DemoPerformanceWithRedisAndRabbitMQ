using DemoPerformanceWithRedisAndRabbitMQ.DAL.Data;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories
{
    /// <summary>
    /// Information about the unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Save changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
    }
}
