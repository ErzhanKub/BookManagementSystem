using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands
{
    public record LoginQuery : IRequest<string>
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }

    internal class LoginHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public LoginHandler(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

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
