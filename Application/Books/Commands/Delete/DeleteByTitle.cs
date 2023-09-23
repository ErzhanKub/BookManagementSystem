using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    public record DeleteBookByTitleCommand : IRequest<bool>
    {
        public required string Title { get; init; }
    }

    internal class DeleteBookByTitleHandler : IRequestHandler<DeleteBookByTitleCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookByTitleHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookByTitleCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(request.Title);
            if (result == true)
                await _unitOfWork.CommitAsync();
            return result;
        }
    }


}
