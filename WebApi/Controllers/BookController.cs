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
            var request = new GetAllRequest();
            var books = await _mediator.Send(request);
            return Ok(books);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
