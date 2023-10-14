using Application.Shared;
using Domain.Repositories;

namespace Application.Baskets.Commands
{
    /// <summary>
    /// Class representing a command to delete books from a user's basket by their titles.
    /// </summary>
    public record DeleteBooksByIdFromBasketCommand : IRequest<bool>
    {
        /// <summary>
        /// The Id of the books to be deleted.
        /// </summary>
        public required Guid[] Id { get; init; }

        /// <summary>
        /// The username of the user whose basket is being modified.
        /// </summary>
        public required string Username { get; init; }
    }

    /// <summary>
    /// Handler for the command to delete books from a user's basket by their titles.
    /// </summary>
    internal class DeleteBooksFromBasket : IRequestHandler<DeleteBooksByIdFromBasketCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="bookRepository">The book repository.</param>
        public DeleteBooksFromBasket(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Handles the command to delete books from a user's basket by their titles.
        /// </summary>
        /// <param name="command">The command to delete books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the books were deleted successfully, false otherwise.</returns>
        public async Task<bool> Handle(DeleteBooksByIdFromBasketCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(command.Username).ConfigureAwait(false);
            var books = await _bookRepository.GetSomeByIdAsync(command.Id).ConfigureAwait(false);

            if (user != null && books != null)
            {
                user.Basket.Books.RemoveAll(book => books.Contains(book));
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }
    }

}
