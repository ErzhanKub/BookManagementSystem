using Application.Orders.Dtos;
using Domain.Repositories;

namespace Application.Orders.Requests
{
    /// <summary>
    /// Query to get all orders for a specific user.
    /// </summary>
    public record GetOrdersForUserQuery : IRequest<IEnumerable<OrderDto>>
    {
        /// <summary>
        /// Gets the username of the user whose orders are to be retrieved.
        /// </summary>
        public required string Username { get; init; }
    }

    /// <summary>
    /// Handles the retrieval of all orders for a specific user.
    /// </summary>
    internal class GetOneOrderHandler : IRequestHandler<GetOrdersForUserQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Initializes a new instance of the GetOneOrderHandler class.
        /// </summary>
        public GetOneOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handles the request to get all orders for a specific user.
        /// </summary>
        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersForUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByUsername(request.Username).ConfigureAwait(false);
            if (orders == null) throw new ArgumentNullException(nameof(orders));
            var response = new List<OrderDto>();
            foreach (var order in orders)
            {
                var result = new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Adress = order.Adress,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    Books = order.Books,
                };
                response.Add(result);
            }
            return response;
        }
    }

}
