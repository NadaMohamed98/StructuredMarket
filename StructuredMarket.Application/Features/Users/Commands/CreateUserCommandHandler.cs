using MediatR;
using Microsoft.AspNetCore.Identity;
using StructuredMarket.Application.Repositories;
using StructuredMarket.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StructuredMarket.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(request.email))
            {
                throw new Exception("Email already exists.");
            }

            // Initialize user without a password hash first
            var user = new User(request.firstName, request.lastName,request.username, request.email, request.phone);

            // Hash the password and assign it
            user.PasswordHash = _passwordHasher.HashPassword(user, request.password);

            // Save to DB
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user.Id;
        }
    }
}
