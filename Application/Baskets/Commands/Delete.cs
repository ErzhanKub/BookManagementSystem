using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Commands
{
    public record DeleteBooksByTitleFromBasketCommand : IRequest<bool>
    {
        public required string[] Title { get; init; }
        public required string Username { get; init; }
    }

    internal class DeleteBooksFromBasket : IRequestHandler<DeleteBooksByTitleFromBasketCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public DeleteBooksFromBasket(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(DeleteBooksByTitleFromBasketCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(command.Username).ConfigureAwait(false);
            var books = await _bookRepository.GetSomeByTitleAsync(command.Title).ConfigureAwait(false);

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
