using MediatR;
using StructuredMarket.Application.Features.Users.Commands;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Application.Features.Users.Queries;

namespace StructuredMarket.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<Guid> RegisterUserAsync(UserDto user)
        {
            var command = new CreateUserCommand(user.FirstName, user.LastName, user.Email, user.Password);

            return await _mediator.Send(command);
        }
    }
}
