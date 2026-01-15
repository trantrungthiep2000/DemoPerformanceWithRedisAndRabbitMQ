using DemoPerformanceWithRedisAndRabbitMQ.DAL.Data;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories
{
    /// <summary>
    /// Information about processed event repository.
    /// </summary>
    public class ProcessedEventRepository : IProcessedEventRepository
    {
        private readonly AppDbContext _dbContext;

        public ProcessedEventRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Is the event already processed.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> IsProcessedAsync(Guid eventId) => await _dbContext.ProcessedEvents.AnyAsync(x => x.EventId == eventId);

        /// <summary>
        /// Mark event as processed.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task MarkProcessedAsync(Guid eventId)
        {
            await _dbContext.ProcessedEvents.AddAsync(new ProcessedEvent { EventId = eventId });
        }
    }
}
