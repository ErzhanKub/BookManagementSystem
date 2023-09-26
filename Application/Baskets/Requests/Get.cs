using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Requests
{
    public record GetBooksFromBasketResponse
    {
        public List<Book> Books { get; init; } = new();
    }

    public record GetBooksFromBasketQuery : IRequest<GetBooksFromBasketResponse?>
    {
        public required string Username { get; init; }
    }

    internal class GetBooksFromBasketHandler : IRequestHandler<GetBooksFromBasketQuery, GetBooksFromBasketResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetBooksFromBasketHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetBooksFromBasketResponse?> Handle(GetBooksFromBasketQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username).ConfigureAwait(false);
            if (user is not null)
            {
                var response = new GetBooksFromBasketResponse();
                response.Books.AddRange(user.Basket.Books.ToList());
                return response;
            }
            return default;
        }
    }

}
