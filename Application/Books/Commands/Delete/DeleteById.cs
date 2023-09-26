using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands.Delete
{
    /// <summary>
    /// Class representing a command to delete books by their IDs.
    /// </summary>
    public record DeleteBooksByIdCommand : IRequest<bool>
    {
        /// <summary>
        /// The IDs of the books to be deleted.
        /// </summary>
        public required Guid[] Id { get; init; }
    }

    /// <summary>
    /// Handler for the command to delete books by their IDs.
    /// </summary>
    internal class DeleteBooksByIdHandler : IRequestHandler<DeleteBooksByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeleteBooksByIdHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete books by their IDs.
        /// </summary>
        /// <param name="command">The command to delete books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the books were deleted successfully, false otherwise.</returns>
        public async Task<bool> Handle(DeleteBooksByIdCommand command, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(command.Id).ConfigureAwait(false);
            if (result)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }


}
