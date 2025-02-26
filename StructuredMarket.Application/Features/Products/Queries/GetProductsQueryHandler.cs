using AutoMapper;
using MediatR;
using StructuredMarket.Application.Features.Products.Models;
using StructuredMarket.Application.Interfaces.Repositories;

namespace StructuredMarket.Application.Features.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductResModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductResModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            var take = request.PageSize;

            var products = await _productRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                products = request.Ascending
                    ? products.OrderBy(p => p.GetType().GetProperty(request.SortBy)?.GetValue(p, null))
                    : products.OrderByDescending(p => p.GetType().GetProperty(request.SortBy)?.GetValue(p, null));
            }

            products = products.Skip(skip).Take(take).ToList();

            return _mapper.Map<List<ProductResModel>>(products);
        }
    }
}
