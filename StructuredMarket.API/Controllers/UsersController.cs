using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StructuredMarket.Application.Common;
using StructuredMarket.Application.Features.Users.Commands;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Application.Features.Users.Queries;
using System;
using System.Threading.Tasks;

namespace StructuredMarket.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[Authorize("USER")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            try
            {
                _logger.LogInformation("Registering user: {Email}", command.email);

                var result = await _mediator.Send(command);

                if (result.Equals(Guid.Empty))
                {
                    _logger.LogWarning("Failed to register user: {Email}", command.email);
                    return BadRequest(ApiResponse<Guid>.FailureResponse("Failed to create user"));
                }

                _logger.LogInformation("User registered successfully: {Email}", command.email);
                return Ok(ApiResponse<Guid>.SuccessResponse(result, "User Created Successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Email}", command.email);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred"));
            }
        }

        //[Authorize("ADMIN")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminCommand command)
        {
            try
            {
                _logger.LogInformation("Registering admin: {Email}", command.email);

                var result = await _mediator.Send(command);

                if (result.Equals(Guid.Empty))
                {
                    _logger.LogWarning("Failed to register admin: {Email}", command.email);
                    return BadRequest(ApiResponse<Guid>.FailureResponse("Failed to create admin"));
                }

                _logger.LogInformation("Admin registered successfully: {Email}", command.email);
                return Ok(ApiResponse<Guid>.SuccessResponse(result, "Admin Created Successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering admin: {Email}", command.email);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred"));
            }
        }

        [Authorize("ADMIN, USER")]
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching user by ID: {Id}", id);

                var query = new GetUserByIdQuery(id);
                var user = await _mediator.Send(query);

                if (user is null)
                {
                    _logger.LogWarning("User not found: {Id}", id);
                    return NotFound(ApiResponse<UserDto>.FailureResponse("User not found"));
                }

                _logger.LogInformation("User retrieved: {Id}, Email: {Email}", user.Id, user.Email);
                return Ok(ApiResponse<UserDto>.SuccessResponse(user, "User retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user by ID: {Id}", id);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred"));
            }
        }

        [Authorize("USER")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery login)
        {
            try
            {
                _logger.LogInformation("User login attempt: {Username}", login.username);

                var user = await _mediator.Send(login);

                if (user is null)
                {
                    _logger.LogWarning("Login failed for user: {Username}", login.username);
                    return Unauthorized(ApiResponse<LoginResModel>.FailureResponse("Invalid username or password"));
                }

                _logger.LogInformation("User logged in successfully: {Username}, Token: {Token}", login.username, user.Token);
                return Ok(ApiResponse<LoginResModel>.SuccessResponse(user, "Login successful"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for user: {Username}", login.username);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred"));
            }
        }
    }
}
