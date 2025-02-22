using StructuredMarket.Application.Interfaces;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
