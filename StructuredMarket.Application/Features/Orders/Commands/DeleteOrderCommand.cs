using MediatR;

namespace StructuredMarket.Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : IRequest<bool>;

}
