using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    public record GetUserResponse
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required Role Role { get; init; }
        public Basket? Basket { get; set; }
    }
    public record GetUserByNameCommand : IRequest<GetUserResponse?> { public required string Username { get; init; } }
    internal class GetUserByNameHandler : IRequestHandler<GetUserByNameCommand, GetUserResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse?> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByName(request.Username);
            if (user != null)
            {
                var response = new GetUserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    Role = user.Role,
                    Basket = user.Basket,
                };
                return response;
            }
            return default;
        }

    }
}
