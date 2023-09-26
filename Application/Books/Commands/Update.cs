using Application.Books.Dtos;
using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    /// <summary>
    /// Class representing a command to update a book.
    /// </summary>
    public record UpdateBookCommand : IRequest<BookDto?>
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
    /// Handler for the command to update a book.
    /// </summary>
    internal class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UpdateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to update a book.
        /// </summary>
        /// <param name="command">The command to update a book.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated book data transfer object or null.</returns>
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
