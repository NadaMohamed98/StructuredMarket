using Microsoft.EntityFrameworkCore;
using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;
namespace StructuredMarket.Infrastructure.Repositories.UserRepo
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StructuredMarketDbContext context) : base(context) { }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Equals(email));
        }
    }
}
