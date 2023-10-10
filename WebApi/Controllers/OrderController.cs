using Application.Orders.Commands;
using Application.Orders.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrderQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("GetOne")]
        public async Task<IActionResult> GetOne(GetOrdersForUserQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(UpdateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteByIdOrder")]
        public async Task<IActionResult> Delete(DeleteOrderByIdCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
