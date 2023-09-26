using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Commands
{

    public record AddBookToBasketCommand : IRequest<bool>
    {
        public required string Title { get; init; }
        public required string Username { get; init; }
    }

    internal class AddBookToBasketHandler : IRequestHandler<AddBookToBasketCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public AddBookToBasketHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

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
