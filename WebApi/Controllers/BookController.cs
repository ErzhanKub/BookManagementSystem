using Application.Books.Commands;
using Application.Books.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAlBooklRequest();
            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [HttpPost("GetByTitle")]
        public async Task<IActionResult> GetByTitle(GetBookByTitleRequest request)
        {
            var result = await _mediator.Send(request);
            if (result != null)
                return Ok(result);
            return BadRequest("Book Not Found");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteByTitle")]
        public async Task<IActionResult> Delete(DeleteBookByTitleCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok();
            return BadRequest("No delete");//TODO
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(DeleteBookByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok();
            return BadRequest("No delete"); //TODO
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateBookCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
