using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Data;

namespace StructuredMarket.Infrastructure.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StructuredMarketDbContext context) : base(context)
        {
        }
    }
}
