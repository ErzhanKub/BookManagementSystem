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
    /// <summary>
    /// The UserController class is responsible for handling user related requests.
    /// </summary>
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="mediator">An instance of IMediator.</param>
        /// <param name="logger">An instance of ILogger.</param>
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        /// <summary>
        /// Retrieves all users. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetByName")]
        /// <summary>
        /// Retrieves a user by their username. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="request">The request containing the username of the user to retrieve.</param>
        /// <returns>The user with the specified username; otherwise, a NotFound response.</returns>
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
        /// <summary>
        /// Retrieves some users by their usernames. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="request">The request containing the usernames of the users to retrieve.</param>
        /// <returns>A list of users with the specified usernames.</returns>
        public async Task<IActionResult> GetSome(GetSomeUsersQuery request)
        {
            if (request.Usernames.IsNullOrEmpty()) return BadRequest("Is null");

            var users = await _mediator.Send(request);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update")]
        /// <summary>
        /// Updates an existing user. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to update a user, containing the new details of the user.</param>
        /// <returns>An Ok response if the user was updated successfully; otherwise, a NotFound response.</returns>
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
        /// <summary>
        /// Deletes a user by their username. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to delete a user, containing the username of the user to delete.</param>
        /// <returns>An Ok response if the user was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(DeleteUsersByNamesCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteById")]
        /// <summary>
        /// Deletes a user by their ID. This method can only be accessed by users with the "Admin" role.
        /// </summary>
        /// <param name="command">The command to delete a user, containing the ID of the user to delete.</param>
        /// <returns>An Ok response if the user was deleted successfully; otherwise, a NotFound response.</returns>
        public async Task<IActionResult> Delete(DeleteUsersByIdCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return NotFound();
        }
    }

}
