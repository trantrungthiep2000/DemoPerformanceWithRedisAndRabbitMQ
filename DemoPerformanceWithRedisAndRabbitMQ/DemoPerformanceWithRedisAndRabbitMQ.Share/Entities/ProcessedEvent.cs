using System.ComponentModel.DataAnnotations;

namespace DemoPerformanceWithRedisAndRabbitMQ.Share.Entities
{
    /// <summary>
    /// Information about a processed event.
    /// </summary>
    public class ProcessedEvent
    {
        [Key]
        public Guid EventId { get; set; }

        public DateTimeOffset ProcessedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
