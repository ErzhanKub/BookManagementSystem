using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Requests
{
    public record GetBooksFromBasketResponse
    {
        public List<Book> Books { get; init; } = new List<Book>();
    }

    public record GetBooksFromBasketCommand : IRequest<GetBooksFromBasketResponse?>
    {
        public required string Username { get; init; }
    }

    internal class GetBooksFromBasketHandler : IRequestHandler<GetBooksFromBasketCommand, GetBooksFromBasketResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetBooksFromBasketHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetBooksFromBasketResponse?> Handle(GetBooksFromBasketCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByNames(request.Username).ConfigureAwait(false);
            if (users is not null)
            {
                List<Book> books = new();
                foreach (var user in users)
                {
                    books = user.Basket.Books;
                }
                if (books is null)
                    return null;
                var response = new GetBooksFromBasketResponse();
                response.Books.AddRange(books);
                return response;
            }
            return default;
        }
    }

}
