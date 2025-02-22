using StructuredMarket.Domain.Entities;
using System.Threading.Tasks;

namespace StructuredMarket.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        Task<int> SaveChangesAsync();
    }
}
