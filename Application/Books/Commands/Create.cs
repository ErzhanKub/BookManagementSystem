using Application.Books.Dtos;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    public record CreateBookCommand : IRequest<BookDto>
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    internal class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = command.Title,
                Description = command.Description,
                Price = command.Price,
            };

            await _bookRepository.CreateAsync(book).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            var response = new BookDto
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
