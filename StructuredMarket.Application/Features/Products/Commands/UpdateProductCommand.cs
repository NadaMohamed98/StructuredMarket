using MediatR;

namespace StructuredMarket.Application.Features.Products.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        string Image,
        decimal Price,
        string Merchant
    ) : IRequest<bool>;
}
