using Application.Books.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    /// <summary>
    /// Class representing a query to get all books.
    /// </summary>
    public record GetAllBooksQuery : IRequest<IEnumerable<BookDto>> { }

    /// <summary>
    /// Handler for the query to get all books.
    /// </summary>
    internal class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Handles the query to get all books.
        /// </summary>
        /// <param name="request">The query to get all books.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of book data transfer objects.</returns>
        public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync().ConfigureAwait(false);
            var response = new List<BookDto>();
            foreach (var book in books)
            {
                var result = new BookDto()
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
