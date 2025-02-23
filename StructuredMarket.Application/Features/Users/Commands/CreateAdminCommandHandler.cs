using MediatR;
using Microsoft.AspNetCore.Identity;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuredMarket.Application.Features.Users.Commands
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateAdminCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(request.email))
            {
                throw new Exception("Email already exists.");
            }

            // Initialize user without a password hash first
            var user = new User(request.firstName, request.lastName, request.username, request.email, request.phone);

            // Hash the password and assign it
            user.PasswordHash = _passwordHasher.HashPassword(user, request.password);

            // Save to DB
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();


            await AssignRoleToUser(user.Id, "ADMIN");

            return user.Id;
        }

        public async Task AssignRoleToUser(Guid userId, string roleName)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (user != null && role != null)
            {
                await _unitOfWork.UserRoles.AddAsync(new UserRole { UserId = user.Id, RoleId = role.Id });
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
