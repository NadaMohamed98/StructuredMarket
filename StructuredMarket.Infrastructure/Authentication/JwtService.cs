using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StructuredMarket.Infrastructure.Authentication
{
    public class JwtService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        public JwtService(JwtTokenSettings jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings;
        }

        public string GenerateToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName+' '+user.LastName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtTokenSettings.Issuer,
                _jwtTokenSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
