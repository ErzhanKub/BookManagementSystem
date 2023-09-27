using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands
{
    /// <summary>
    /// Command to delete an order by its ID.
    /// </summary>
    public record DeleteOrderByIdCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets the ID of the order to be deleted.
        /// </summary>
        public required Guid Id { get; init; }
    }

    /// <summary>
    /// Handles the deletion of an order.
    /// </summary>
    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderByIdCommand, bool>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the DeleteOrderHandler class.
        /// </summary>
        public DeleteOrderHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the request to delete an order.
        /// </summary>
        public async Task<bool> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Delete(request.Id);
            if (response)
                await _unitOfWork.CommitAsync();
            return response;
        }
    }

}
