using AutoMapper;
using MediatR;
using StructuredMarket.Application.Features.Orders.Models;
using StructuredMarket.Application.Features.Orders.Queries;
using StructuredMarket.Application.Interfaces.Services;

namespace StructuredMarket.Application
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);
            if (order == null) throw new KeyNotFoundException("Order not found");

            return _mapper.Map<OrderModel>(order);
        }
    }

}
