using DemoPerformanceWithRedisAndRabbitMQ.Bussiness.Services.Interfaces;
using DemoPerformanceWithRedisAndRabbitMQ.Share.DTOs;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Entities;
using DemoPerformanceWithRedisAndRabbitMQ.Share.Enums;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DemoPerformanceWithRedisAndRabbitMQ.Api.Controllers
{
    /// <summary>
    /// Information about the users controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPublishEndpoint _publish;
        private readonly IUserService _userService;

        public UsersController(IPublishEndpoint publish, IUserService userService)
        {
            _publish = publish;
            _userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            var evt = new UserChangedEvent
            {
                UserId = user.Id,
                Action = ActionTypeEnum.CREATED,
                Name = user.Name,
                Email = user.Email
            };

            await _publish.Publish(evt);
            return Ok();
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(long id, [FromBody] User user)
        {
            var evt = new UserChangedEvent
            {
                UserId = id,
                Action = ActionTypeEnum.UPDATED,
                Name = user.Name,
                Email = user.Email
            };

            await _publish.Publish(evt);
            return Ok();
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(long id)
        {
            var evt = new UserChangedEvent
            {
                UserId = id,
                Action = ActionTypeEnum.DELETED
            };

            await _publish.Publish(evt);
            return Ok();
        }
    }
}
