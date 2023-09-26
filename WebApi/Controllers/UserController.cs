using Application.Users.Commands;
using Application.Users.Commands.Delete;
using Application.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetUserByNameQuery request)
        {
            if (request.Username.IsNullOrEmpty()) return BadRequest("Is null");
            var user = await _mediator.Send(request);
            if (user is not null)
                return Ok(user);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetUsersByNames")]
        public async Task<IActionResult> GetSome(GetSomeUsersQuery request)
        {
            if (request.Usernames.IsNullOrEmpty()) return BadRequest("Is null");

            var users = await _mediator.Send(request);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            if (command.Username.IsNullOrEmpty()) return BadRequest("Is null");
            var user = await _mediator.Send(command);
            if (user is not null)
                return Ok(user);
            return NotFound();
        }

        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        //[HttpPost("UpdateCorrentUser")]
        //public async Task<IActionResult> UpdateCorrentUser(UpdateUserCommand command) // Create new updateCorrent user command
        //{
        //    var currentUsername = HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        //    command.Username = currentUsername;
        //    var updateUser = await _mediator.Send(command);
        //    if (updateUser is not null) return Ok(updateUser);
        //    return NotFound();
        //}

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteByNames")]
        public async Task<IActionResult> Delete(DeleteUsersByNamesCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(DeleteUsersByIdCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }
    }
}
