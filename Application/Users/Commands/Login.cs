using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands
{
    /// <summary>
    /// Class representing a query to perform user login.
    /// </summary>
    public record LoginQuery : IRequest<string>
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; init; }
    }

    /// <summary>
    /// Handler for the query to perform user login.
    /// </summary>
    internal class LoginHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="userRepository">The user repository.</param>
        public LoginHandler(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Handles the query to perform user login.
        /// </summary>
        /// <param name="request">The query to perform user login.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The JWT token string or an error message.</returns>
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CheckUserCredentialsAsync(request.Username, request.Password);
            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };
                var tokenString = GetTokenString(claims, DateTime.UtcNow.AddMinutes(30));
                return tokenString;
            }
            return "User not found";
        }

        private string GetTokenString(List<Claim> claims, DateTime exp)
        {
            var key = _configuration["Jwt"] ?? throw new Exception();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: exp,
                signingCredentials: new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256));

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
