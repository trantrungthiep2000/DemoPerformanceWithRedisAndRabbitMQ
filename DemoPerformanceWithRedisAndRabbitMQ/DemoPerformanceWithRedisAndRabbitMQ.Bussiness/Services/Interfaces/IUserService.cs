using DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;

namespace DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services.Interfaces
{
    /// <summary>
    /// Information about the interface user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Handle the user changed event.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        Task HandleAsync(UserChangedEvent evt);

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync();

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(long id);
    }
}
