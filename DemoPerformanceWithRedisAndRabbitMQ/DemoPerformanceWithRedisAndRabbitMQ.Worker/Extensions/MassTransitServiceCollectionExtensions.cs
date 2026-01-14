using DemoPerformanceWithRedisAndRabbitMQ.Worker.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace DemoPerformanceWithRedisAndRabbitMQ.Worker.Extensions
{
    /// <summary>
    /// Information about MassTransit service collection extensions.
    /// </summary>
    public static class MassTransitServiceCollectionExtensions
    {
        /// <summary>
        /// Adds RabbitMQ MassTransit to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMqMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserWriteConsumer>();
                x.AddConsumer<UserCacheConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("user-write-queue", e =>
                    {
                        e.ConfigureConsumer<UserWriteConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("user-cache-queue", e =>
                    {
                        e.ConfigureConsumer<UserCacheConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
