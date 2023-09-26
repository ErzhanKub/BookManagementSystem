using Application.Books.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    /// <summary>
    /// Class representing a query to get a book by its title.
    /// </summary>
    public record GetBookByTitleQuery : IRequest<BookDto>
    {
        /// <summary>
        /// The title of the book to be retrieved.
        /// </summary>
        public required string Title { get; init; }
    }

    /// <summary>
    /// Handler for the query to get a book by its title.
    /// </summary>
    internal class GetBookByTitle : IRequestHandler<GetBookByTitleQuery, BookDto?>
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public GetBookByTitle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Handles the query to get a book by its title.
        /// </summary>
        /// <param name="request">The query to get a book.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The book data transfer object or null.</returns>
        public async Task<BookDto?> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitleAsync(request.Title).ConfigureAwait(false);
            if (book is not null)
            {
                var response = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                };
                return response;
            }
            return default;
        }
    }

}
