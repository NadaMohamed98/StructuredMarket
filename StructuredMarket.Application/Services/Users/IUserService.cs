using StructuredMarket.Application.Features.Users.Models;

namespace StructuredMarket.Application.Users.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<Guid> RegisterUserAsync(UserDto user);
    }
}
