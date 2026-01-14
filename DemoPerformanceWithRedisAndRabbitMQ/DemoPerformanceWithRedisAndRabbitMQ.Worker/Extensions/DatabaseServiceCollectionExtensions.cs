using DemoPerformanceWithRedisAndRabbitMQ.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DemoPerformanceWithRedisAndRabbitMQ.Worker.Extensions
{
    /// <summary>
    /// Information about database service collection extensions.
    /// </summary>
    public static class DatabaseServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the database to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite(configuration.GetConnectionString("Default")));

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]!);
            });

            return services;
        }
    }
}
