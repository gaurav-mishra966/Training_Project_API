using CreditCard_Backend_API.Data;
using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CreditCard_Backend_API.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    //Id = Guid.TryParse(u.Id, out Guid parsedId) ? parsedId : Guid.Empty,
                    //Id=u.Id,
                    Username = (u.UserName ?? "Unknown").ToUpper(),  // Fallback for null Username
                    Email = u.Email ?? "Unknown",  // Fallback for null Email
                    Status = u.LockoutEnabled ? "InActive" : "Active",
                    PhoneNumber = u.PhoneNumber ?? "XX-XXXXXXXXXX"  // Fallback for null PhoneNumber
                })
                .ToListAsync(); // Fetching all users without pagination
            
            if (users.Count == 0)
            {
                // Add a log message for debugging
                //return BadRequest(new { });
            }
            return users;
        }
    }
}
