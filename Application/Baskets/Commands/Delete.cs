using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Commands
{
    public record DeleteBooksByTitlesFromBasketCommand : IRequest<bool>
    {
        public required string[] Titles { get; init; }
        public required string Username { get; init; }
    }

    internal class DeleteBooksFromBasket : IRequestHandler<DeleteBooksByTitlesFromBasketCommand, bool>
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

        public async Task<bool> Handle(DeleteBooksByTitlesFromBasketCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(request.Username).ConfigureAwait(false);
            var books = await _bookRepository.GetSomeByTitles(request.Titles).ConfigureAwait(false);

            if (user != null && books != null)
            {
                user.Basket.Books.RemoveAll(book => books.Contains(book));
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;

            //user.Basket.Books.ForEach(book =>
            //{
            //    book.Title = request.Titles[1];
            //});
        }

    }
}
