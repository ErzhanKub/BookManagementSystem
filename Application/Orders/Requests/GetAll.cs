using Application.Orders.Dtos;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Requests
{
    public record GetAllOrderQuery : IRequest<IEnumerable<OrderDto>> { }
    internal class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAll().ConfigureAwait(false);
            var response = new List<OrderDto>();
            if(orders.Any())
            {
                foreach (var order in orders)
                {
                    var result = new OrderDto
                    {
                        Id= order.Id,
                        UserId= order.UserId,
                        Adress= order.Adress,
                        Status= order.Status,
                        Books= order.Books,
                        TotalPrice= order.TotalPrice,
                    };
                    response.Add(result);
                }
                return response;
            }
            return default;
        }
    }
}
