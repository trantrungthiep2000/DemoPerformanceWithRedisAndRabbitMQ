namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Information about the interface base repository.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="E"></typeparam>
    public interface IBaseRepository<K,E>
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<E>> GetAllAsync();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<E?> GetByIdAsync(K id);

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(E user);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateAsync(E user);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task DeleteAsync(E user);
    }
}
