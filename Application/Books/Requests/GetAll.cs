using Application.Books.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    public record GetAllBooksQuery : IRequest<IEnumerable<BookDto>> { }

    internal class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

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
