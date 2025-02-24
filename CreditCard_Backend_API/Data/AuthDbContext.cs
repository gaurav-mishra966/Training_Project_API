using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CreditCard_Backend_API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Variables for static reader role IDs
            var adminRoleId = "b89cf111-a855-475c-a0c7-285422563fc8";
            var moderatorRoleId = "42ebdeef-39f8-4a6b-a3ab-3d48ffafeaf2";
            var userRoleId = "0e0d75ca-4402-458c-9dbd-c73fb258f639";
            var guestRoleId = "bfdcffca-2254-4d8c-9ffd-b611b3f4f680";

            // Defining roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                   Id = adminRoleId,
                   Name = "admin",
                   NormalizedName = "admin".ToUpper(),
                   ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                   Id = moderatorRoleId,
                   Name = "moderator",
                   NormalizedName = "moderator".ToUpper(),
                   ConcurrencyStamp = moderatorRoleId
                },
                new IdentityRole()
                {
                   Id = userRoleId,
                   Name = "user",
                   NormalizedName = "user".ToUpper(),
                   ConcurrencyStamp = userRoleId
                },
                new IdentityRole()
                {
                   Id = guestRoleId,
                   Name = "guest",
                   NormalizedName = "guest".ToUpper(),
                   ConcurrencyStamp = guestRoleId
                }
            };

            // Seeding roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Creating Admin user
            var adminUserId = "b89cf111-a855-475c-a0c7-285422563fc8";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Gaurav Mishra",
                Email = "gaurav@testapp.com",
                NormalizedEmail = "gaurav@testapp.com".ToUpper(),
                NormalizedUserName = "Gaurav Mishra".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Test@123");

            // Creating additional users
            var users = new List<IdentityUser>
            {
                new IdentityUser()
                {
                    Id = "d110fc00-9f4b-4ff6-9a42-e2bb49c13b82",
                    UserName = "johndoe",
                    Email = "john.doe@testapp.com",
                    NormalizedEmail = "john.doe@testapp.com".ToUpper(),
                    NormalizedUserName = "johndoe".ToUpper()
                },
                new IdentityUser()
                {
                    Id = "a1b3c4d5-2f68-4878-b3b8-d65c3f377599",
                    UserName = "janesmith",
                    Email = "jane.smith@testapp.com",
                    NormalizedEmail = "jane.smith@testapp.com".ToUpper(),
                    NormalizedUserName = "janesmith".ToUpper()
                },
                new IdentityUser()
                {
                    Id = "f01c2d3a-e428-4a42-8393-c659d4b83ea1",
                    UserName = "robertbrown",
                    Email = "robert.brown@testapp.com",
                    NormalizedEmail = "robert.brown@testapp.com".ToUpper(),
                    NormalizedUserName = "robertbrown".ToUpper()
                }
            };

            // Hashing passwords for the additional users
            foreach (var user in users)
            {
                user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "Test@1234");
            }

            // Seeding admin and other users
            builder.Entity<IdentityUser>().HasData(admin);
            builder.Entity<IdentityUser>().HasData(users);

            // Roles Allocation
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = "d110fc00-9f4b-4ff6-9a42-e2bb49c13b82",  // John Doe
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = "a1b3c4d5-2f68-4878-b3b8-d65c3f377599",  // Jane Smith
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = "f01c2d3a-e428-4a42-8393-c659d4b83ea1",  // Robert Brown
                    RoleId = userRoleId
                }
            };

            // Seeding user roles
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
