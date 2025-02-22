using MediatR;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>;
