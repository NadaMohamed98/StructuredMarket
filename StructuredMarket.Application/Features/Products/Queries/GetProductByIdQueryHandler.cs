using AutoMapper;
using MediatR;
using StructuredMarket.Application.Features.Products.Models;
using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Application.Features.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return _mapper.Map<ProductResModel>(product);
        }
    }
}
