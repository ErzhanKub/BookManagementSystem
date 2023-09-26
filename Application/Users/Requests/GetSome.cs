using Application.Users.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    public record GetSomeUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public required string[] Usernames { get; init; }
    }

    internal class GetSomeUsersHandler : IRequestHandler<GetSomeUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetSomeUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetSomeUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetSomeUsersByNamesAsync(request.Usernames).ConfigureAwait(false);
            var response = new List<UserDto>();
            foreach (var user in users)
            {
                var result = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                };
                response.Add(result);
            }
            return response;
        }
    }
}
