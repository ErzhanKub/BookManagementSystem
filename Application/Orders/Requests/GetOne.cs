using Application.Orders.Dtos;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Requests
{
    public record GetOneOrderQuery : IRequest<OrderDto>
    {
        public required string Username { get; init; }
    }

    internal class GetOneOrderHandler : IRequestHandler<GetOneOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOneOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOneOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByUsername(request.Username).ConfigureAwait(false);
            if (order == null) throw new ArgumentNullException(nameof(order));
            var response = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Adress = order.Adress,
                Status = order.Status,
                Books = order.Books,
                TotalPrice = order.TotalPrice,
            };
            return response;
        }
    }
}
