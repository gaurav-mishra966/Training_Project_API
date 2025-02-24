using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories;
using CreditCard_Backend_API.Repositories.Interface;
using CreditCard_Backend_API.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard_Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var response = new UserListResponse
            {
                TotalCount = users.Count,
                Users = users
            };

            // If no users were found, return a NotFound result
            if (users.Count == 0)
            {
                return NotFound("No users found.");
            }

            // Return OK with the response containing both the total count and users
            return Ok(response);
        }

    }
}
