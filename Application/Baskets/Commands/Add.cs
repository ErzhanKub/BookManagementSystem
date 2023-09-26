using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Commands
{

    /// <summary>
    /// Class representing a command to add a book to a user's basket.
    /// </summary>
    public record AddBookToBasketCommand : IRequest<bool>
    {
        /// <summary>
        /// The title of the book to be added.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// The username of the user whose basket is being modified.
        /// </summary>
        public required string Username { get; init; }
    }

    /// <summary>
    /// Handler for the command to add a book to a user's basket.
    /// </summary>
    internal class AddBookToBasketHandler : IRequestHandler<AddBookToBasketCommand, bool>
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
        public AddBookToBasketHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Handles the command to add a book to a user's basket.
        /// </summary>
        /// <param name="command">The command to add a book.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the book was added successfully, false otherwise.</returns>
        public async Task<bool> Handle(AddBookToBasketCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(command.Username).ConfigureAwait(false);
            var book = await _bookRepository.GetByTitleAsync(command.Title).ConfigureAwait(false);

            if (user is not null && book is not null)
            {
                user.Basket.Books.Add(book);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }
    }


}
