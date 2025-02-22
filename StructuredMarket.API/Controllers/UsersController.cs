using MediatR;
using Microsoft.AspNetCore.Mvc;
using StructuredMarket.Application.Features.Users.Commands;
using StructuredMarket.Application.Features.Users.Queries;

namespace StructuredMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Register " + command.email);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Done register " + command.email);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            return user != null ? Ok(user) : NotFound();
        }
    }
}
