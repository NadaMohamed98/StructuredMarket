using StructuredMarket.Application.Interfaces;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.PermissionRepo
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
