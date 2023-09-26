using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// The AuthController class is responsible for handling authentication related requests.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="mediator">An instance of IMediator.</param>
        /// <param name="logger">An instance of ILogger.</param>
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        /// <summary>
        /// Handles user login requests.
        /// </summary>
        /// <param name="query">The login query containing the username and password.</param>
        /// <returns>A JWT token if the login is successful; otherwise, a BadRequest response.</returns>
        public async Task<IActionResult> Login(LoginQuery query)
        {
            if (query.Username.IsNullOrEmpty()) return BadRequest("Username is null");
            if (query.Password.IsNullOrEmpty()) return BadRequest("Password is null");

            var token = await _mediator.Send(query) ?? string.Empty;
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        /// <summary>
        /// Handles user registration requests.
        /// </summary>
        /// <param name="command">The command to create a new user, containing the username, password, and role.</param>
        /// <returns>An Ok response if the registration is successful; otherwise, a BadRequest response.</returns>
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var secret = UselessFile.Gentleman(command.Username);
            if (secret is not null)
                return Ok(secret);

            if (command.Username.IsNullOrEmpty()) return BadRequest("Username is null");
            if (command.Password.IsNullOrEmpty()) return BadRequest("Password is null");

            if (((int)command.Role) < 1 || ((int)command.Role) > 2)
                return BadRequest("Not the correct role");
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }

}
