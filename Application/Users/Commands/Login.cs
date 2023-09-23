using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands
{
    public record LoginCommand : IRequest<string>
    {
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
    }

    internal class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public LoginHandler(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByName(request.Username);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Username),
                };
                var tokenString = GetTokenString(claims, DateTime.UtcNow.AddMinutes(30));
                return Task.FromResult(tokenString);
            }
            return Task.FromResult("Not Found");
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
