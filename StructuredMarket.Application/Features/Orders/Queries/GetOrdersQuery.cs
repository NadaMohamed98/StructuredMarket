using MediatR;
using StructuredMarket.Application.Features.Orders.Models;

namespace StructuredMarket.Application.Features.Orders.Queries
{
    public record GetOrdersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<OrderModel>>;

}
