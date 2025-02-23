using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user, List<string> roles);
    }
}