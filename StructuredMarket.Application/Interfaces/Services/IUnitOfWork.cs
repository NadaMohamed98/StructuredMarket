using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Domain.Entities;
using System.Threading.Tasks;

namespace StructuredMarket.Application.Interfaces.Services
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        IRolePermissionRepository RolePermissions { get; }
        IUserRoleRepository UserRoles { get; }
        Task<int> SaveChangesAsync();
    }
}
