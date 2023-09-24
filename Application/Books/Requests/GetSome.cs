using Application.Users.Requests;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public record GetSomeBooksResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
    public record GetSomeBookByTitlesRequest : IRequest<IEnumerable<GetSomeBooksResponse>>
    {
        public required string[] Title { get; init; }
    }

    internal class GetSomeBooksHandler : IRequestHandler<GetSomeBookByTitlesRequest, IEnumerable<GetSomeBooksResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetSomeBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<GetSomeBooksResponse>> Handle(GetSomeBookByTitlesRequest request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetSomeByTitles(request.Title);
            var response = new List<GetSomeBooksResponse>();
            foreach (var book in books)
            {
                var result = new GetSomeBooksResponse
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
