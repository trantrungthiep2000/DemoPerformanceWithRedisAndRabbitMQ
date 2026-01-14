using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Information about the interface user repository.
    /// </summary>
    public interface IUserRepository : IBaseRepository<long, User> { }
}
