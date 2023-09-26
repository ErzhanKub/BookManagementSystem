using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Requests
{
    public record GetBooksFromBasketResponse
    {
        public List<Book> Books { get; init; } = new();
    }

    public record GetBooksFromBasketRequest : IRequest<GetBooksFromBasketResponse?>
    {
        public required string Username { get; init; }
    }

    internal class GetBooksFromBasketHandler : IRequestHandler<GetBooksFromBasketRequest, GetBooksFromBasketResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetBooksFromBasketHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetBooksFromBasketResponse?> Handle(GetBooksFromBasketRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetSomeUsersByNames(request.Username).ConfigureAwait(false);
            if (users is not null)
            {
                var response = new GetBooksFromBasketResponse();
                foreach (var user in users)
                {
                    response.Books.AddRange(user.Basket.Books.ToList());
                }
                return response;
            }
            return default;
        }
    }

}
