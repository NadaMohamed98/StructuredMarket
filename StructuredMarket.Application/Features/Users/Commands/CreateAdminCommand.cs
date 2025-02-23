using MediatR;

namespace StructuredMarket.Application.Features.Users.Commands
{
    public record CreateAdminCommand(string firstName, string lastName, string username, string email, string phone, string password) : IRequest<Guid>;
}
