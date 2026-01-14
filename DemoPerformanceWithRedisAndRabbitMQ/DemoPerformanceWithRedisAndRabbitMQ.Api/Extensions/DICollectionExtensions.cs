using DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services;
using DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;

namespace DemoPerformanceWithRedisAndRabbitMQ.Api.Extensions
{
    /// <summary>
    /// Information about DI service collection extensions.
    /// </summary>
    public static class DICollectionExtensions
    {
        /// <summary>
        /// Adds Dependency Injection to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProcessedEventRepository, ProcessedEventRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
