using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Infrastructure.Data;
using StructuredMarket.Infrastructure.Repositories.OrderItemRepo;
using StructuredMarket.Infrastructure.Repositories.OrderRepo;
using StructuredMarket.Infrastructure.Repositories.PermissionRepo;
using StructuredMarket.Infrastructure.Repositories.ProductRepo;
using StructuredMarket.Infrastructure.Repositories.RoleRepo;
using StructuredMarket.Infrastructure.Repositories.UserRepo;

namespace StructuredMarket.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StructuredMarketDbContext _context;

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }

        public IPermissionRepository Permissions { get; }

        public IOrderItemRepository OrderItems { get; }

        public IRolePermissionRepository RolePermissions { get; }

        public IUserRoleRepository UserRoles { get; }

        public UnitOfWork(StructuredMarketDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
            Permissions = new PermissionRepository(context);
            Products = new ProductRepository(context);
            Orders = new OrderRepository(context);
            OrderItems = new OrderItemRepository(context);
            UserRoles = new UserRoleRepository(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
