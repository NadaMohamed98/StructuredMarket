using StructuredMarket.Application.Interfaces;
using StructuredMarket.Application.Repositories;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;
using StructuredMarket.Infrastructure.Repositories.OrderItemRepo;
using StructuredMarket.Infrastructure.Repositories.OrderRepo;
using StructuredMarket.Infrastructure.Repositories.PermissionRepo;
using StructuredMarket.Infrastructure.Repositories.ProductRepo;
using StructuredMarket.Infrastructure.Repositories.RoleRepo;
using StructuredMarket.Infrastructure.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UnitOfWork(StructuredMarketDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
            Permissions = new PermissionRepository(context);
            Products = new ProductRepository(context);
            Orders = new OrderRepository(context);
            OrderItems = new OrderItemRepository(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
