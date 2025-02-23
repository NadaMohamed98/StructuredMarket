using MediatR;
using StructuredMarket.Application.Interfaces.Services;

namespace StructuredMarket.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            var existingOrders = await _unitOfWork.OrderItems.AnyAsync(o => o.ProductId == request.Id);

            if (existingOrders)
                throw new InvalidOperationException("Cannot delete a product that is included in existing orders.");

            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
