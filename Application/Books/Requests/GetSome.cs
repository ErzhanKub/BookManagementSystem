using Application.Books.Dtos;
using Domain.Repositories;

namespace Application.Books.Requests
{
    /// <summary>
    /// Class representing a query to get books by their titles.
    /// </summary>
    public record GetBooksByTitleQuery : IRequest<IEnumerable<BookDto>>
    {
        /// <summary>
        /// The titles of the books to be retrieved.
        /// </summary>
        public required string[] Title { get; init; }
    }

    /// <summary>
    /// Handler for the query to get books by their titles.
    /// </summary>
    internal class GetBooksHandler : IRequestHandler<GetBooksByTitleQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public GetBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Handles the query to get books by their titles.
        /// </summary>
        /// <param name="request">The query to get books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of book data transfer objects.</returns>
        public async Task<IEnumerable<BookDto>> Handle(GetBooksByTitleQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetSomeByTitleAsync(request.Title).ConfigureAwait(false);
            var response = new List<BookDto>();
            foreach (var book in books)
            {
                var result = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                };
                response.Add(result);
            }
            return response;
        }
    }

}
