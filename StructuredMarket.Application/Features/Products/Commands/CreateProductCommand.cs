using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuredMarket.Application.Features.Products.Commands
{
    public record CreateProductCommand(
            string Name,
            string Description,
            string Image,
            decimal Price,
            string Merchant
        ) : IRequest<Guid>;
}
