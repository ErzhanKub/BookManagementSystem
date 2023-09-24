using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    public record GetBooksResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    public record GetAllBooksRequest : IRequest<IEnumerable<GetBooksResponse>> { }

    internal class GetAllBookHandler : IRequestHandler<GetAllBooksRequest, IEnumerable<GetBooksResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<GetBooksResponse>> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync().ConfigureAwait(false);
            var response = new List<GetBooksResponse>();
            foreach (var book in books)
            {
                var result = new GetBooksResponse()
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
