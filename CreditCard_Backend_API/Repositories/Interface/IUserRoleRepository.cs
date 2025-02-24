namespace CreditCard_Backend_API.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        Task<string> GetUserRoleAsync(int userId);
    }
}
