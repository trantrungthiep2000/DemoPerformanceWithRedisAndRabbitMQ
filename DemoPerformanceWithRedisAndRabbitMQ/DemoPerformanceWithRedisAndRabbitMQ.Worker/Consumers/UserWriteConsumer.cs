using DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs;
using MassTransit;

namespace DemoPerformanceWithRedisAndRabbitMQ.Worker.Consumers
{
    /// <summary>
    /// Information about the user write consumer.
    /// </summary>
    public class UserWriteConsumer : IConsumer<UserChangedEvent>
    {
        private readonly IUserService _userService;

        public UserWriteConsumer(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Consumes the user changed event.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UserChangedEvent> context)
        {
            await _userService.HandleAsync(context.Message);
        }
    }
}
