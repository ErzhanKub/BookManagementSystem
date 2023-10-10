using Application.Orders.Dtos;
using Domain.Repositories;

namespace Application.Orders.Requests
{
    /// <summary>
    /// Query to get all orders.
    /// </summary>
    public record GetAllOrderQuery : IRequest<IEnumerable<OrderDto>> { }

    /// <summary>
    /// Handles the retrieval of all orders.
    /// </summary>
    internal class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Initializes a new instance of the GetAllOrderHandler class.
        /// </summary>
        public GetAllOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handles the request to get all orders.
        /// </summary>
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAll().ConfigureAwait(false);
            var response = new List<OrderDto>();
            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    var result = new OrderDto
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        Adress = order.Adress,
                        Status = order.Status,
                        Books = order.Books,
                        TotalPrice = order.TotalPrice,
                    };
                    response.Add(result);
                }
                return response;
            }
            return default!;
        }
    }

}
