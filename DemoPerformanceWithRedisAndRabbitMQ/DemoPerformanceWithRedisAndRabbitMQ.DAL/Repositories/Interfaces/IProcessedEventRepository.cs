namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Information about interface processed event repository.
    /// </summary>
    public interface IProcessedEventRepository
    {
        /// <summary>
        /// Is the event already processed.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task<bool> IsProcessedAsync(Guid eventId);

        /// <summary>
        /// Mark event as processed.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkProcessedAsync(Guid eventId);
    }
}
