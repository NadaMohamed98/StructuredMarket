using MediatR;
using StructuredMarket.Application.Features.Orders.Models;

namespace StructuredMarket.Application.Features.Orders.Queries
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderModel>;

}
