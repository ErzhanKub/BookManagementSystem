using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Baskets.Requests
{
    /// <summary>
    /// Class representing a response object that contains a list of books.
    /// </summary>
    public record GetBooksFromBasketResponse
    {
        /// <summary>
        /// The list of books.
        /// </summary>
        public List<Book> Books { get; init; } = new();
    }

    /// <summary>
    /// Class representing a query to get books from a user's basket.
    /// </summary>
    public record GetBooksFromBasketQuery : IRequest<GetBooksFromBasketResponse?>
    {
        /// <summary>
        /// The name of the user whose basket is being queried.
        /// </summary>
        public required string Username { get; init; }
    }

    /// <summary>
    /// Handler for the query to get books from a user's basket.
    /// </summary>
    internal class GetBooksFromBasketHandler : IRequestHandler<GetBooksFromBasketQuery, GetBooksFromBasketResponse?>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public GetBooksFromBasketHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the query to get books from a user's basket.
        /// </summary>
        /// <param name="request">The query to get books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object that contains a list of books or null.</returns>
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
