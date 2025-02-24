using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard_Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            
        }

        [HttpGet]
        [Route("user/{email}")]
        //[Authorize]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            // Attempt to find the user by email
            var user = await userManager.FindByEmailAsync(email);

            // If user is not found, return NotFound
            if (user == null)
            {
                return NotFound(new { Message = $"User with email {email} not found." });
            }

            // Get the user's roles
            var roles = await userManager.GetRolesAsync(user);

            // Return user details with roles in the response
            var userDto = new
            {
                user.UserName,
                user.Email,
                Roles = roles
            };

            return Ok(new { Message = "✅ User Found", User = userDto });
        }



        [HttpPost]
        [Route("user/{email}")]
        //[Authorize]
        public async Task<IActionResult> GetUserProfileByEmail([FromBody] string email)
        {
            // Attempt to find the user by email
            var user = await userManager.FindByEmailAsync(email);

            // If user is not found, return NotFound
            if (user == null)
            {
                return NotFound(new { Message = $"User with email {email} not found." });
            }

            // Get the user's roles
            var roles = await userManager.GetRolesAsync(user);

            // Return user details with roles in the response
            var userDto = new
            {
                user.UserName,
                user.Email,
                Roles = roles
            };

            return Ok(new { Message = "✅ User Found", User = userDto });
        }


        //Post: {apiBaseUrl/api/auth/login}
        [HttpPost]
        [Route("validateUser/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            //check Email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser != null)
            {
                //check password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    //get user roles
                    var roles = await userManager.GetRolesAsync(identityUser);

                    //Create token and response
                    var jwtToken = tokenRepository.CreateJWTToken(identityUser, roles.ToList());

                    //create token and generate response
                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };
                    return Ok(response);
                }
            }
            else
            {
                ModelState.AddModelError("Invalid user credentials", "Email or Password invalid");
            }
            return ValidationProblem(ModelState);
        }


        [HttpPost]
        [Route("/reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Email and new password are required.");
            }

            // Find the user by email
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Reset the user's password
            var resetPasswordResult = await userManager.RemovePasswordAsync(user);
            if (!resetPasswordResult.Succeeded)
            {
                return BadRequest("Error removing current password.");
            }

            var addPasswordResult = await userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                return BadRequest("Error setting the new password.");
            }

            return Ok("Password successfully reset.");
        }

        [HttpPost]
        [Route("logout")]
        //[Authorize]
        public IActionResult Logout()
        {
            // You can clear the token or mark the token as invalid

            // Here we simply return a success message as the JWT is client-managed
            return Ok(new { message = "User logged out successfully" });
        }


    }
}
