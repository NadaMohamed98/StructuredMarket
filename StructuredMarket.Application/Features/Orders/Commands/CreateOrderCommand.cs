using MediatR;
using StructuredMarket.Application.Features.Orders.Models;

namespace StructuredMarket.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(
        Guid UserId,
        string DeliveryAddress,
        decimal TotalCost,
        DateTime DeliveryTime,
        List<OrderDetailsModel> OrderDetails
    ) : IRequest<Guid>;


}
