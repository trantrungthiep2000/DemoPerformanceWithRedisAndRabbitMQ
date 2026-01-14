using DemoPerformanceWithRedisAndRabbitMQ.Share.Enums;

namespace DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs
{
    /// <summary>
    /// Information about a user change event.
    /// </summary>
    public record UserChangedEvent
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public long UserId { get; init; }
        public ActionTypeEnum Action { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}
