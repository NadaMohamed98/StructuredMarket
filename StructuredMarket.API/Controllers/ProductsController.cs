using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StructuredMarket.Application.Common;
using StructuredMarket.Application.Features.Products.Commands;
using StructuredMarket.Application.Features.Products.Models;
using StructuredMarket.Application.Features.Products.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StructuredMarket.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                _logger.LogInformation("Creating product: {ProductName}", command.Name);

                var productId = await _mediator.Send(command);

                _logger.LogInformation("Product created successfully: {ProductId}", productId);
                return Ok(ApiResponse<Guid>.SuccessResponse(productId, "Product created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product: {ProductName}", command.Name);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred."));
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
        {
            try
            {
                _logger.LogInformation("Fetching all products.");

                var products = await _mediator.Send(query);

                _logger.LogInformation("Retrieved {ProductCount} products successfully.", products.Count);
                return Ok(ApiResponse<List<ProductResModel>>.SuccessResponse(products, "Products retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred."));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching product by ID: {ProductId}", id);

                var product = await _mediator.Send(new GetProductByIdQuery(id));

                if (product is null)
                {
                    _logger.LogWarning("Product not found: {ProductId}", id);
                    return NotFound(ApiResponse<string>.FailureResponse("Product not found."));
                }

                _logger.LogInformation("Product retrieved: {ProductId}", id);
                return Ok(ApiResponse<ProductResModel>.SuccessResponse(product, "Product retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product: {ProductId}", id);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred."));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            try
            {
                _logger.LogInformation("Updating product: {ProductId}", command.Id);

                await _mediator.Send(command);

                _logger.LogInformation("Product updated successfully: {ProductId}", command.Id);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Product updated successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Product not found: {ProductId}", command.Id);
                return NotFound(ApiResponse<string>.FailureResponse("Product not found."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product: {ProductId}", command.Id);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred."));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting product: {ProductId}", id);

                await _mediator.Send(new DeleteProductCommand(id));

                _logger.LogInformation("Product deleted successfully: {ProductId}", id);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Product not found: {ProductId}", id);
                return NotFound(ApiResponse<string>.FailureResponse("Product not found."));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Invalid operation while deleting product: {ProductId}", id);
                return BadRequest(ApiResponse<string>.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product: {ProductId}", id);
                return StatusCode(500, ApiResponse<string>.FailureResponse("An unexpected error occurred."));
            }
        }
    }
}
