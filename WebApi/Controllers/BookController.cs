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
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllBooksQuery();
            var books = await _mediator.Send(request);
            if (books is null) return Ok("The list is empty");
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpPost("GetByTitle")]
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
        public async Task<IActionResult> GetSome(GetBooksByTitleQuery request)
        {
            if (request.Title.IsNullOrEmpty()) return BadRequest("Title is null");

            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
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
        public async Task<IActionResult> Delete(DeleteBooksByTitleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book(s) deleted.");
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(DeleteBooksByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book(s) deleted.");
            return NotFound();
        }
    }
}
