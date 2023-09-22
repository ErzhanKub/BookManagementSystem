using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public record GetBooksResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
    public record GetAlBooklRequest : IRequest<IEnumerable<GetBooksResponse>> { }
    
    internal class GetAllBookHandler : IRequestHandler<GetAlBooklRequest, IEnumerable<GetBooksResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<GetBooksResponse>> Handle(GetAlBooklRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
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
