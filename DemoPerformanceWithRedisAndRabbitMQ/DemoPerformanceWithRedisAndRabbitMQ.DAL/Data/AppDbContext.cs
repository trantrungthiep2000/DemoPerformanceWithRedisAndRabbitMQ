using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoPerformanceWithRedisAndRabbitMQ.DAL.Data
{
    /// <summary>
    /// Information about the data database context.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ProcessedEvent> ProcessedEvents { get; set; }
    }
}
