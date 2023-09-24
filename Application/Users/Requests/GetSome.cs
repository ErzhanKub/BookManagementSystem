using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    public record GetSomeUsersResponse
    {
        public Guid Id { get; init; }
        public required string Username { get; init; }
        public required Role Role { get; init; }
    }
    public record GetSomeUsersCommand : IRequest<IEnumerable<GetSomeUsersResponse>>
    {
        public required string[] Username { get; init; }
    }

    internal class GetSomeUsersHandler : IRequestHandler<GetSomeUsersCommand, IEnumerable<GetSomeUsersResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetSomeUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetSomeUsersResponse>> Handle(GetSomeUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetSomeUsersByNames(request.Username);
            var response = new List<GetSomeUsersResponse>();
            foreach (var user in users)
            {
                var result = new GetSomeUsersResponse
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
