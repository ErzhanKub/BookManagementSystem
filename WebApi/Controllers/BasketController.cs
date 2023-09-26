using Application.Baskets.Commands;
using Application.Baskets.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// The BasketController class is responsible for handling basket related requests.
    /// </summary>
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        /// <summary>
        /// Initializes a new instance of the BasketController class.
        /// </summary>
        /// <param name="mediator">An instance of IMediator.</param>
        /// <param name="logger">An instance of ILogger.</param>
        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetForUser")]
        /// <summary>
        /// Retrieves the books in the basket for the currently logged in user.
        /// </summary>
        /// <returns>A list of books in the user's basket; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Get()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var request = new GetBooksFromBasketQuery
            {
                Username = username
            };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("GetForAdmin")]
        /// <summary>
        /// Retrieves the books in the basket for a specified user. This method is intended for use by administrators.
        /// </summary>
        /// <param name="request">The request containing the username of the user whose basket to retrieve.</param>
        /// <returns>A list of books in the specified user's basket.</returns>
        public async Task<IActionResult> GetForAdmin(GetBooksFromBasketQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("AddForUser")]
        /// <summary>
        /// Adds a book to the basket for the currently logged in user.
        /// </summary>
        /// <param name="title">The title of the book to add to the basket.</param>
        /// <returns>An Ok response if the book was added successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Add(string title)
        {
            if (title.IsNullOrEmpty()) return BadRequest("Title is null");

            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var command = new AddBookToBasketCommand
            {
                Title = title,
                Username = username
            };
            var response = await _mediator.Send(command);
            if (response)
                return Ok("Book add basket");
            return NotFound("Book not Found");
        }

        [HttpPost("AddForAdmin")]
        /// <summary>
        /// Adds a book to the basket for a specified user. This method is intended for use by administrators.
        /// </summary>
        /// <param name="command">The command to add a book to a user's basket, containing the title of the book and the username of the user.</param>
        /// <returns>An Ok response if the book was added successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Add(AddBookToBasketCommand command)
        {
            if (command.Title.IsNullOrEmpty()) return BadRequest("Title is null");
            if (command.Username.IsNullOrEmpty()) return BadRequest("Username is null");

            var response = await _mediator.Send(command);
            if (response)
                return Ok("Book add basket");
            return NotFound("Book not Found");
        }

        [HttpDelete("DeleteForAdmin")]
        /// <summary>
        /// Deletes a book from a specified user's basket. This method is intended for use by administrators.
        /// </summary>
        /// <param name="command">The command to delete a book from a user's basket, containing the title of the book and the username of the user.</param>
        /// <returns>An Ok response if the book was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(DeleteBooksByTitleFromBasketCommand command)
        {
            var response = await _mediator.Send(command);
            if (response) return Ok("Book(s) deleted");
            return NotFound();
        }

        [HttpPost("DeleteForUser")]
        /// <summary>
        /// Deletes a book from the currently logged in user's basket.
        /// </summary>
        /// <param name="title">The title of the book to delete from the basket.</param>
        /// <returns>An Ok response if the book was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(string[] title)
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();

            var command = new DeleteBooksByTitleFromBasketCommand
            {
                Title = title,
                Username = username
            };

            var response = await _mediator.Send(command);
            if (response) return Ok("Book");
            return NotFound();
        }
    }

}
