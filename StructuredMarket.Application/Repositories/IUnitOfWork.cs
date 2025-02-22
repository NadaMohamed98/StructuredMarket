using StructuredMarket.Application.Interfaces;
using StructuredMarket.Domain.Entities;
using System.Threading.Tasks;

namespace StructuredMarket.Application.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        Task<int> SaveChangesAsync();
    }
}
