using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    public record DeleteBookByIdCommand : IRequest<bool>
    {
        public required Guid[] Id { get; init; }
    }

    internal class DeleteBookByIdHandler : IRequestHandler<DeleteBookByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookByIdHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(request.Id).ConfigureAwait(false);
            if (result)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }

}
