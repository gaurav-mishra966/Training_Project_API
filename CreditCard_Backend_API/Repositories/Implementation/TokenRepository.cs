using System.Text;
using CreditCard_Backend_API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace CreditCard_Backend_API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        string ITokenRepository.CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //create claims from roles
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //use claims and define JWT Token parametes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(-1),
                signingCredentials: credentials);

            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
