using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.RoleRepo
{
    public class RolePermissionsRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionsRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
