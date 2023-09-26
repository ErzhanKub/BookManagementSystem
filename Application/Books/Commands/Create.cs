using Application.Books.Dtos;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    /// <summary>
    /// Class representing a command to create a book.
    /// </summary>
    public record CreateBookCommand : IRequest<BookDto>
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// The description of the book.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// The price of the book.
        /// </summary>
        public decimal Price { get; init; }
    }

    /// <summary>
    /// Handler for the command to create a book.
    /// </summary>
    internal class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CreateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to create a book.
        /// </summary>
        /// <param name="command">The command to create a book.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created book data transfer object.</returns>
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
