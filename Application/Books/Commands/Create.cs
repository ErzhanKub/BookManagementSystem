using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    public record CreateBookResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    public record CreateBookCommand : IRequest<CreateBookResponse>
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    internal class CreateBookHandler : IRequestHandler<CreateBookCommand, CreateBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
            };

            await _bookRepository.CreateAsync(book).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            var response = new CreateBookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
            };

            return response;
        }
    }

}
