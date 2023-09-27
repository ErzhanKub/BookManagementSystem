using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands
{
    public record UpdateOrderCommand : IRequest<Order>
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public required string Adress { get; init; }
        public decimal TotalPrice { get; init; }
        public OrderStatus Status { get; init; }
    }

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

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
            return default;
        }
    }
}
