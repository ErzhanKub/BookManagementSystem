using Application.Books.Dtos;
using Application.Extensions;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;


namespace Application.Books.Commands
{
    /// <summary>
    /// Class representing a command to create a book.
    /// </summary>
    public record CreateBookCommand : IRequest<Result<BookDto>>
    {
        public BookDto? Book { get; set; }
    }

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(c => c.Book)
                .NotNull();

            When(c => c.Book != null,
                () =>
                {
                    RuleFor(c => c.Book!.Title)
                        .NotEmpty()
                        .WithMessage("{PropertyName} обязательное поле")
                        .Length(1, 200);

                    RuleFor(c => c.Book!.Price).GreaterThanOrEqualTo(0);
                });
        }
    }

    /// <summary>
    /// Handler for the command to create a book.
    /// </summary>
    internal class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<BookDto>>
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
        public async Task<Result<BookDto>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = command.Book.Title,
                Description = command.Book.Description,
                Price = command.Book.Price,
            };

            await _bookRepository.CreateAsync(book).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            var response = book.Adapt<BookDto>();

            return Result.Ok(response);
        }
    }
}
