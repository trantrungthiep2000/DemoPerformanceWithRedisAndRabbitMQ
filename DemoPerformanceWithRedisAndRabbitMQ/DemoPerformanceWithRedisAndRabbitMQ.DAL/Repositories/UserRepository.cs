using DemoPerformanceWithRedisAndRabbitMQ.DAL.Data;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories
{
    /// <summary>
    /// Information about User repository.
    /// </summary>
    public class UserRepository : BaseRepository<long, User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
