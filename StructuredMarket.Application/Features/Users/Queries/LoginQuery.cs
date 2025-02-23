using MediatR;
using StructuredMarket.Application.Features.Users.Models;

namespace StructuredMarket.Application.Features.Users.Queries
{
    public record LoginQuery
    (
        string username,
        string password
    ) : IRequest<LoginResModel>;
}
