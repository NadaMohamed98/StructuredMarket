using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructuredMarket.Application.Common;
using StructuredMarket.Application.Features.Users.Commands;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Application.Features.Users.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StructuredMarket.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersV1Controller> _logger;

        public UsersV1Controller(IMediator mediator, ILogger<UsersV1Controller> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize("USER")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Register " + command.email);

            var result = await _mediator.Send(command);

            if (result.Equals(Guid.Empty))
            {
                _logger.LogInformation("failed to register " + command.email);
                return Ok(ApiResponse<Guid>.FailureResponse("Failed to create user"));
            }

            _logger.LogInformation("Done register " + command.email);

            return Ok(ApiResponse<Guid>.SuccessResponse(result, "User Created Successfully"));
        }

        [Authorize("ADMIN, USER")]
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            _logger.LogInformation("User: " + id);

            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if(user is null)
            {
                _logger.LogInformation("failed to get user: " + id.ToString());
                return Ok(ApiResponse<UserDto>.FailureResponse("Failed to get user"));
            }

            _logger.LogInformation("Done: " + user.Id + " " + user.Email);

            return Ok(ApiResponse<UserDto>.SuccessResponse(user, "success"));
        }

        [Authorize("USER")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery login)
        {
            _logger.LogInformation("Login User: " + login.username);

            var user = await _mediator.Send(login);
            if (user is null)
            {
                _logger.LogInformation("failed to login user: " + login.username);
                return Ok(ApiResponse<LoginResModel>.FailureResponse("Failed to login"));
            }
            _logger.LogInformation("token: " + user.Token);

            return Ok(ApiResponse<LoginResModel>.SuccessResponse(user, "success"));
        }
    }
}
