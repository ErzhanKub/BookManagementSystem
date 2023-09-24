using Application.Baskets.Commands;
using Application.Baskets.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("AddBookToBasket")]
        public async Task<IActionResult> Add(string title)
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();
            var request = new AddBookToBasketCommand
            {
                Title = title,
                Username = username
            };
            var response = await _mediator.Send(request);
            if (response)
                return Ok("Book add basket");
            return BadRequest("");
        }

        [HttpPost("GetBooksFromBasket")]
        public async Task<IActionResult> Get()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            if (username is null)
                return NotFound();
            var request = new GetBooksFromBasketCommand
            {
                Username = username
            };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
