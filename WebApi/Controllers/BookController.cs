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

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAlBooklRequest();
            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [HttpPost("GetByTitle")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTitle(GetBookByTitleRequest request)
        {
            var result = await _mediator.Send(request);
            if (result != null)
                return Ok(result);
            return BadRequest("Book Not Found");
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateBookCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null) return Ok(response);
            return BadRequest();
        }

        [HttpDelete("DeleteByTitle")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteBookByTitleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book deleted.");
            return BadRequest("Book not found.");
        }

        [HttpDelete("DeleteById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteBookByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok("Book deleted.");
            return BadRequest("Book not found.");
        }
    }
}
