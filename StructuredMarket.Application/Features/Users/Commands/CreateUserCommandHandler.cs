using MediatR;
using Microsoft.AspNet.Identity;
using StructuredMarket.Domain.Entities;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new Exception("Email already exists.");
        }

        var user = new User(request.FirstName, request.LastName, request.Email, _passwordHasher.HashPassword(request.Password));
        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
