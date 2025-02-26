using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructuredMarket.Application.Common;
using StructuredMarket.Application.Features.Orders.Commands;
using StructuredMarket.Application.Features.Orders.Models;
using StructuredMarket.Application.Features.Orders.Queries;

namespace StructuredMarket.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                _logger.LogInformation("Creating order for User: {UserId}", command.UserId);
                var orderId = await _mediator.Send(command);

                return Ok(ApiResponse<Guid>.SuccessResponse(orderId, "Order created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                return BadRequest(ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            try
            {
                _logger.LogInformation("Updating order: {OrderId}", command.Id);
                var result = await _mediator.Send(command);

                if (result != false)
                    Ok(ApiResponse<bool>.SuccessResponse(true, "Order updated successfully."));

                return BadRequest(ApiResponse<string>.FailureResponse("Order update failed."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order.");
                return BadRequest(ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery query)
        {
            try
            {
                _logger.LogInformation("Fetching orders.");
                var orders = await _mediator.Send(query);

                return Ok(ApiResponse<List<OrderModel>>.SuccessResponse(orders, "Orders retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders.");
                return BadRequest(ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching order: {OrderId}", id);
                var order = await _mediator.Send(new GetOrderByIdQuery(id));

                return Ok(ApiResponse<OrderModel>.SuccessResponse(order, "Order retrieved successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Order not found.");
                return NotFound(ApiResponse<string>.FailureResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting order: {OrderId}", id);
                var result = await _mediator.Send(new DeleteOrderCommand(id));

                if (result != false)
                    Ok(ApiResponse<bool>.SuccessResponse(true, "Order deleted successfully."));

                return BadRequest(ApiResponse<string>.FailureResponse("Order cannot be deleted."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order.");
                return BadRequest(ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }
    }

}
