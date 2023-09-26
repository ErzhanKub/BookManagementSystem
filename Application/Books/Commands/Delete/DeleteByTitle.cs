using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    /// <summary>
    /// Class representing a command to delete books by their titles.
    /// </summary>
    public record DeleteBooksByTitleCommand : IRequest<bool>
    {
        /// <summary>
        /// The titles of the books to be deleted.
        /// </summary>
        public required string[] Title { get; init; }
    }

    /// <summary>
    /// Handler for the command to delete books by their titles.
    /// </summary>
    internal class DeleteBooksByTitleHandler : IRequestHandler<DeleteBooksByTitleCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeleteBooksByTitleHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete books by their titles.
        /// </summary>
        /// <param name="command">The command to delete books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the books were deleted successfully, false otherwise.</returns>
        public async Task<bool> Handle(DeleteBooksByTitleCommand command, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(command.Title).ConfigureAwait(false);
            if (result)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }

}
