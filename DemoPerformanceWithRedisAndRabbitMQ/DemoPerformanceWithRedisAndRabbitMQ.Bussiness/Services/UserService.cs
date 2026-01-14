using DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.DAL.Repositories.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Commons;
using DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Enums;
using StackExchange.Redis;
using System.Text.Json;

namespace DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services
{
    /// <summary>
    /// Information about the user service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProcessedEventRepository _processedEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatabase _redis;

        public UserService(IUserRepository userRepository,
            IProcessedEventRepository processedEventRepository,
            IUnitOfWork unitOfWork,
            IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _processedEventRepository = processedEventRepository;
            _unitOfWork = unitOfWork;
            _redis = redis.GetDatabase();
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            // Get from cache
            var cachedUsers = await _redis.StringGetAsync(Constants.USER_LIST_KEY);

            if (!cachedUsers.IsNullOrEmpty && cachedUsers.HasValue)
            {
                var json = cachedUsers.ToString();
                return JsonSerializer.Deserialize<IEnumerable<User>>(json)!;
            }

            // Cache miss, get from database
            var users = await _userRepository.GetAllAsync();

            // Set to cache
            await _redis.StringSetAsync(Constants.USER_LIST_KEY, 
                JsonSerializer.Serialize(users), TimeSpan.FromMinutes(5));

            return users;
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(long id)
        {
            var key = $"user:{id}";

            // Get from cache
            var cachedUsers = await _redis.StringGetAsync(key);

            if (!cachedUsers.IsNullOrEmpty && cachedUsers.HasValue)
            {
                var json = cachedUsers.ToString();
                return JsonSerializer.Deserialize<User>(json)!;
            }

            // Cache miss, get from database
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) { throw new Exception($"User with ID {id} not found."); }

            // Set to cache
            await _redis.StringSetAsync(key, JsonSerializer.Serialize(user), TimeSpan.FromMinutes(5));

            return user;
        }

        /// <summary>
        /// Handle the user changed event.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public async Task HandleAsync(UserChangedEvent evt)
        {
            if (await _processedEventRepository.IsProcessedAsync(evt.EventId)) { return; }

            switch (evt.Action)
            {
                case ActionTypeEnum.CREATED:
                    await CreateAsync(evt);
                    break;

                case ActionTypeEnum.UPDATED:
                    await UpdateAsync(evt);
                    break;

                case ActionTypeEnum.DELETED:
                    await DeleteAsync(evt);
                    break;
            }

            await _processedEventRepository.MarkProcessedAsync(evt.EventId);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        private async Task CreateAsync(UserChangedEvent evt)
        {
            var user = new User { Name = evt.Name, Email = evt.Email };
            await _userRepository.AddAsync(user);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task UpdateAsync(UserChangedEvent evt)
        {
            var user =  await _userRepository.GetByIdAsync(evt.UserId);
            if (user is null) { throw new Exception($"User with ID {evt.UserId} not found."); }

            user.Name = evt.Name;
            user.Email = evt.Email;
            user.UpdatedAt = DateTimeOffset.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        private async Task DeleteAsync(UserChangedEvent evt)
        {
            var user = await _userRepository.GetByIdAsync(evt.UserId);
            if (user is null) { throw new Exception($"User with ID {evt.UserId} not found."); }

            await _userRepository.DeleteAsync(user);
        }
    }
}
