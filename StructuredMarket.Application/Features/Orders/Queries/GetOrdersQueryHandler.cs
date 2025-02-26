using AutoMapper;
using MediatR;
using StructuredMarket.Application.Features.Orders.Models;
using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Application.Interfaces.Services;

namespace StructuredMarket.Application.Features.Orders.Queries
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            var take = request.PageSize;

            var orders = await _unitOfWork.Orders.GetAllAsync();

            orders = orders.Skip(skip).Take(take).ToList();

            return _mapper.Map<List<OrderModel>>(orders);
        }
    }

}
