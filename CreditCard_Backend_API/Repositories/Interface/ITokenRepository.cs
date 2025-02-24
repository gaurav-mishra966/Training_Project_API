using Microsoft.AspNetCore.Identity;

namespace CreditCard_Backend_API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
