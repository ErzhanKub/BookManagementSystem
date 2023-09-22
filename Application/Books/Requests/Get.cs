using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public record GetBookResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
    public record GetAlBooklRequest : IRequest<IEnumerable<GetBookResponse>> { }
    public record GetBookByTitleRequest : IRequest<GetBookResponse> { public required string Title { get; init; } }
    internal class GetBookByTitle : IRequestHandler<GetBookByTitleRequest, GetBookResponse>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByTitle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<GetBookResponse> Handle(GetBookByTitleRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitle(request.Title);
            var response = new GetBookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
            };
            return response;
        }
    }
    internal class GetAllBookHandler : IRequestHandler<GetAlBooklRequest, IEnumerable<GetBookResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<GetBookResponse>> Handle(GetAlBooklRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
            var response = new List<GetBookResponse>();
            foreach (var book in books)
            {
                var result = new GetBookResponse()
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
