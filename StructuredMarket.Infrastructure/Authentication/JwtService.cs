using Microsoft.IdentityModel.Tokens;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StructuredMarket.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        public JwtService(JwtTokenSettings jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings;
        }

        public string GenerateToken(User user, List<string> roles)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtTokenSettings.Key);
            var expiryMinutes = int.Parse(_jwtTokenSettings.ExpiryMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add role claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = _jwtTokenSettings.Issuer,
                Audience = _jwtTokenSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}
