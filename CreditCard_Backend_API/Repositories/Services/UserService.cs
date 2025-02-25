using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories.Interface;

namespace CreditCard_Backend_API.Repositories.UserServices
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
