namespace CreditCard_Backend_API.Repositories.Implementation
{
    public class UserRoleRepository
    {
        public async Task<string> GetUserRoleAsync(int userId)
        {
            // For demonstration purposes, assume userId = 1 is an admin
            return await Task.FromResult(userId == 1 ? "admin" : "guest");
        }
    }
}
