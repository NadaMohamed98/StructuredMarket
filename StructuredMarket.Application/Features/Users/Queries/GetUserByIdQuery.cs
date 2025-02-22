using MediatR;
using StructuredMarket.Application.Features.Users.Models;
namespace StructuredMarket.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
}