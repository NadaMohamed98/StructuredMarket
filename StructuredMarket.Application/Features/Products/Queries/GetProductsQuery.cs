using MediatR;
using StructuredMarket.Application.Features.Products.Models;

namespace StructuredMarket.Application.Features.Products.Queries
{
    public record GetProductsQuery(int PageNumber = 1, int PageSize = 10, string? SortBy = null, bool Ascending = true)
            : IRequest<List<ProductResModel>>;
}
