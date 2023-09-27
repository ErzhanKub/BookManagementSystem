using Application.Orders.Dtos;
using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands
{
    /// <summary>
    /// Command to create a new order.
    /// </summary>
    public record CreateOrderCommand : IRequest<OrderDto>
    {
        /// <summary>
        /// Gets the username of the user placing the order.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// Gets the address where the order should be delivered.
        /// </summary>
        public required string Adress { get; init; }
    }

    /// <summary>
    /// Handles the creation of a new order.
    /// </summary>
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the CreateOrderHandler class.
        /// </summary>
        public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the request to create a new order.
        /// </summary>
        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username);
            if (user is null) throw new ArgumentNullException(nameof(user));

            var books = user.Basket.Books.ToList();
            if (!books.Any()) throw new ArgumentNullException(nameof(books));

            var price = books.Sum(book => book.Price);

            var order = new Order
            {
                UserId = user.Id,
                Adress = request.Adress,
                Books = books,
                TotalPrice = price,
                Status = OrderStatus.delivered,
                User = user,
            };

            var orderDto = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Adress = order.Adress,
                Books = order.Books,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
            };

            await _orderRepository.Create(order);
            user.Basket.Books.Clear();
            await _unitOfWork.CommitAsync();

            return orderDto;
        }
    }

}
