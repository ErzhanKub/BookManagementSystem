using Application.Books.Commands;
using Application.Books.Commands.Delete;
using Application.Books.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var request = new GetAllBooksRequest();
            var books = await _mediator.Send(request);
            if (books is null) return Ok("The list is empty");
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpPost("GetByTitle")]
        public async Task<IActionResult> GetByTitle(GetBookByTitleRequest request)
        {
            var book = await _mediator.Send(request);
            if (book is not null)
                return Ok(book);
            return BadRequest("Book Not Found");
        }

        [AllowAnonymous]
        [HttpPost("GetSomeByTitles")]
        public async Task<IActionResult> GetSome(GetSomeBookByTitlesRequest request)
        {
            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateBookCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null) return Ok(response);
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteByTitle")]
        public async Task<IActionResult> Delete(DeleteBookByTitleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book deleted.");
            return BadRequest("Book not found.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(DeleteBookByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book deleted.");
            return BadRequest("Book not found.");
        }
    }
}
