namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Information about the interface unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
