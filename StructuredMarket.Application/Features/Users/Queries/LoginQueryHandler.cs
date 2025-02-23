using MediatR;
using Microsoft.AspNetCore.Identity;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Application.Features.Users.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResModel>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginQueryHandler(IJwtService jwtService, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IPasswordHasher<User> passwordHasher)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Username.Equals(request.username));
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.password);

            if (user == null || passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var roles = user.UserRoles.Select(r => r.Role.Name).ToList();
            var token = _jwtService.GenerateToken(user, roles);
            var result = new LoginResModel
            {
                Token = token
            };

            return result;
        }
    }
}
