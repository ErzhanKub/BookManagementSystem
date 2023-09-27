using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Commands
{
    public record DeleteOrderByIdCommand : IRequest<bool>
    {
        public required Guid Id { get; init; }
    }

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderByIdCommand, bool>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Delete(request.Id);
            if (response)
                await _unitOfWork.CommitAsync();
            return response;
        }
    }
}
