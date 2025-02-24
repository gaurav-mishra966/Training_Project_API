using Microsoft.AspNetCore.Identity;

namespace CreditCard_Backend_API.Models.Domain
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        //public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
