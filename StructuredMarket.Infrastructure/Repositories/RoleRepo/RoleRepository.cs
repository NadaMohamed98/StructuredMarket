using StructuredMarket.Application.Interfaces;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.RoleRepo
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
