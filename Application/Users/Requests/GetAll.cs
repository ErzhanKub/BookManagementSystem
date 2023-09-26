using Application.Users.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>> { }

    internal class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(false);
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
