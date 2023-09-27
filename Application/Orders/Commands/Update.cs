using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands
{
    /// <summary>
    /// Command to update an existing order.
    /// </summary>
    public record UpdateOrderCommand : IRequest<Order>
    {
        /// <summary>
        /// Gets the ID of the order to be updated.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the ID of the user who placed the order.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Gets the address where the order should be delivered.
        /// </summary>
        public required string Adress { get; init; }

        /// <summary>
        /// Gets the total price of the order.
        /// </summary>
        public decimal TotalPrice { get; init; }

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        public OrderStatus Status { get; init; }
    }

    /// <summary>
    /// Handles the updating of an order.
    /// </summary>
    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the UpdateOrderHandler class.
        /// </summary>
        public UpdateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the request to update an order.
        /// </summary>
        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.Id).ConfigureAwait(false);
            if (order is not null)
            {
                order.UserId = request.UserId;
                order.Adress = request.Adress;
                order.Status = request.Status;
                order.TotalPrice = request.TotalPrice;

                _orderRepository.Update(order);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);

                return order;
            }
            return default!;
        }
    }

}
