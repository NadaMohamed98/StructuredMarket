﻿using MediatR;

namespace StructuredMarket.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}
