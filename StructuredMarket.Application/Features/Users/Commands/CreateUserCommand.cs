using MediatR;
namespace StructuredMarket.Application.Features.Users.Commands
{
    public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>;
}
