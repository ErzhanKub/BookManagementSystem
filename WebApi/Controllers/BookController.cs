using Application.Books.Commands;
using Application.Books.Commands.Delete;
using Application.Books.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// The BookController class is responsible for handling book related requests.
    /// </summary>
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the BookController class.
        /// </summary>
        /// <param name="logger">An instance of ILogger.</param>
        /// <param name="mediator">An instance of IMediator.</param>
        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A list of all books; otherwise, a message indicating that the list is empty.</returns>
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllBooksQuery();
            var books = await _mediator.Send(request);
            if (books is null) return Ok("The list is empty");
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpPost("GetByTitle")]
        /// <summary>
        /// Retrieves a book by its title.
        /// </summary>
        /// <param name="request">The request containing the title of the book to retrieve.</param>
        /// <returns>The book with the specified title; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> GetByTitle(GetBookByTitleQuery request)
        {
            if (request.Title.IsNullOrEmpty()) return BadRequest("Title is null");

            var easterEgg = UselessFile.NeverGiveUp(request.Title);
            if (easterEgg is not null)
                return Ok(easterEgg);

            var book = await _mediator.Send(request);
            if (book is not null)
                return Ok(book);
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("GetSomeByTitle")]
        /// <summary>
        /// Retrieves some books by their title.
        /// </summary>
        /// <param name="request">The request containing the title of the books to retrieve.</param>
        /// <returns>A list of books with the specified title.</returns>
        public async Task<IActionResult> GetSome(GetBooksByTitleQuery request)
        {
            if (request.Title.IsNullOrEmpty()) return BadRequest("Title is null");

            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        /// <summary>
        /// Creates a new book. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to create a new book, containing the title and price of the book.</param>
        /// <returns>An Ok response if the book was created successfully; otherwise, a BadRequest response.</returns>
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            if (command.Title.IsNullOrEmpty()) return BadRequest("Title is null");
            if (command.Price < 0)
                return BadRequest($"Price cannot be negative {command.Price}");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update")]
        /// <summary>
        /// Updates an existing book. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to update a book, containing the new details of the book.</param>
        /// <returns>An Ok response if the book was updated successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Update(UpdateBookCommand command)
        {
            if (command.Title.IsNullOrEmpty())
                return BadRequest("Title cannot be empty");

            var response = await _mediator.Send(command);
            if (response is not null) return Ok(response);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteByTitle")]
        /// <summary>
        /// Deletes a book by its title. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to delete a book, containing the title of the book to delete.</param>
        /// <returns>An Ok response if the book was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(DeleteBooksByTitleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book(s) deleted.");
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        /// <summary>
        /// Deletes a book by its ID. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to delete a book, containing the ID of the book to delete.</param>
        /// <returns>An Ok response if the book was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(DeleteBooksByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book(s) deleted.");
            return NotFound();
        }
    }

}
