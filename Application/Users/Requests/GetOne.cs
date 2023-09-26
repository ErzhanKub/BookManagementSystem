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
        public required Role Role { get; init; }
        public Basket? Basket { get; set; }
    }
    public record GetUserByNameQuery : IRequest<GetUserResponse?> { public required string Username { get; init; } }
    internal class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, GetUserResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username).ConfigureAwait(false);
            if (user is not null)
            {
                var response = new GetUserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                    Basket = user.Basket,
                };
                return response;
            }
            return default;
        }
    }
}
