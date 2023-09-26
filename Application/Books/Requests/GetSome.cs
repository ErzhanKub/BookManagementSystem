using Application.Books.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    public record GetBooksByTitleQuery : IRequest<IEnumerable<BookDto>>
    {
        public required string[] Title { get; init; }
    }

    internal class GetBooksHandler : IRequestHandler<GetBooksByTitleQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

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
