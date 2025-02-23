using AutoMapper;
using MediatR;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Application.Interfaces.Repositories;

namespace StructuredMarket.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId)
                       ?? throw new Exception("User not found.");

            return _mapper.Map<UserDto>(user);
        }
    }
}
