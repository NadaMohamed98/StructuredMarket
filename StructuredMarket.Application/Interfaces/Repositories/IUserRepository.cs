using StructuredMarket.Domain.Entities;
namespace StructuredMarket.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmailAsync(string email);
    }
}
