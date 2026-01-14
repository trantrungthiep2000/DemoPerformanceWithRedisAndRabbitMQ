using System.ComponentModel.DataAnnotations;

namespace DemoPerformanceWithRedisAndRabbitMQ.Share.Entities
{
    /// <summary>
    /// Information about a user.
    /// </summary>
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
