using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.OrderItemRepo
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
