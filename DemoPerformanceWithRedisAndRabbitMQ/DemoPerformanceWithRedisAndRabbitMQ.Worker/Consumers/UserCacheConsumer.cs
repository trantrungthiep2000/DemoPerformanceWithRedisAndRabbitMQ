using DemoPerformanceWithRedisAndRabbitMQ.Share.Commons;
using DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Enums;
using MassTransit;
using StackExchange.Redis;

namespace DemoPerformanceWithRedisAndRabbitMQ.Worker.Consumers
{
    /// <summary>
    /// Information about the user cache consumer.
    /// </summary>
    public class UserCacheConsumer : IConsumer<UserChangedEvent>
    {
        private readonly IDatabase _redis;

        public UserCacheConsumer(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        /// <summary>
        /// Consumes the user changed event and deletes the corresponding user cache from Redis.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UserChangedEvent> context)
        {
            await _redis.KeyDeleteAsync(Constants.USER_LIST_KEY);

            if (context.Message.Action != ActionTypeEnum.CREATED)
            {
                await _redis.KeyDeleteAsync($"{Constants.USER}:{context.Message.UserId}");
            }
        }
    }
}
