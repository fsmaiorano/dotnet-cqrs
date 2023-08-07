using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IBlogDataContext _context;

        public AuthService(IConfiguration config, IBlogDataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<string?> HandleUserAuthentication(UserEntity user)
        {
            try
            {
                var storedUser = await _context.Users.FirstOrDefaultAsync(l => l.Email == user.Email && l.PasswordHash == user.PasswordHash);

                if (storedUser == null)
                {
                    return string.Empty;
                }

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, storedUser.Name!),
                new Claim(JwtRegisteredClaimNames.Email, storedUser.Email!),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);

                return stringToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
