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
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetForUser")]
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
        public async Task<IActionResult> GetForAdmin(GetBooksFromBasketQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("AddForUser")]
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
        public async Task<IActionResult> Delete(DeleteBooksByTitleFromBasketCommand command)
        {
            var response = await _mediator.Send(command);
            if (response) return Ok("Book(s) deleted");
            return NotFound();
        }

        [HttpPost("DeleteForUser")]
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
