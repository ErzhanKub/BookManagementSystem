using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public record GetAllResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
    public record GetAllRequest : IRequest<IEnumerable<GetAllResponse>> { }

    internal class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
            var response = new List<GetAllResponse>();
            foreach (var book in books)
            {
                var result = new GetAllResponse()
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
