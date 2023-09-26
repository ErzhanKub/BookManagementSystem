using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    public record DeleteBooksByIdCommand : IRequest<bool>
    {
        public required Guid[] Id { get; init; }
    }

    internal class DeleteBooksByIdHandler : IRequestHandler<DeleteBooksByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBooksByIdHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBooksByIdCommand command, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(command.Id).ConfigureAwait(false);
            if (result)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }

}
