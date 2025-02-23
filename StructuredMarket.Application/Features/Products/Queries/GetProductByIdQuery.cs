using MediatR;
using StructuredMarket.Application.Features.Products.Models;

namespace StructuredMarket.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResModel>;
}
