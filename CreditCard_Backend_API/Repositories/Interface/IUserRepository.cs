using CreditCard_Backend_API.Models.DTO;

namespace CreditCard_Backend_API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
