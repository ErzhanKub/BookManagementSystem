using Application.Books.Dtos;
using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    public record UpdateBookCommand : IRequest<BookDto?>
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    internal class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto?> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitleAsync(command.Title).ConfigureAwait(false);
            if (book is not null)
            {
                book.Title = command.Title;
                book.Description = command.Description;
                book.Price = command.Price;

                _bookRepository.Update(book);
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
            return default;
        }
    }

}
