using MediatR;
using StructuredMarket.Application.Features.Orders.Models;

namespace StructuredMarket.Application.Features.Orders.Commands
{
    public record UpdateOrderCommand(
        Guid Id,
        Guid UserId,
        string DeliveryAddress,
        decimal TotalAmount,
        DateTime DeliveryTime,
        List<OrderDetailsModel> OrderDetails
    ) : IRequest<bool>;


}
