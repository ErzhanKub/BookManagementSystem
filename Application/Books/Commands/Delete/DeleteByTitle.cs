using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    public record DeleteBooksByTitleCommand : IRequest<bool>
    {
        public required string[] Title { get; init; }
    }

    internal class DeleteBooksByTitleHandler : IRequestHandler<DeleteBooksByTitleCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBooksByTitleHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBooksByTitleCommand command, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(command.Title).ConfigureAwait(false);
            if (result)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }
}
