using Application.Common.Interfaces;
using Application.UseCases.User.Queries.GetUser;
using Microsoft.AspNetCore.DataProtection;
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

        public AuthService(IConfiguration config, IBlogDataContext context)
        {
            _config = config;
        }

        public async Task<string?> GenerateToken(UserAuthenticationDto user)
        {
            try
            {
                if (user.Id == 0 || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return null;

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()!),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name!),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                     }),
                    Expires = DateTime.UtcNow.AddMinutes(50),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                                            (new SymmetricSecurityKey(key),
                                             SecurityAlgorithms.HmacSha512Signature)
                };
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return stringToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateToken(string token)
        {
            try
            {
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
