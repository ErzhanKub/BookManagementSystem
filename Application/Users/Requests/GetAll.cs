using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    public record GetUsersResponse
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; init; }
        public Basket? Basket { get; set; }
    }
    public record GetAllUsersRequest : IRequest<IEnumerable<GetUsersResponse>> { }

    internal class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<GetUsersResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var response = new List<GetUsersResponse>();
            foreach (var user in users)
            {
                var result = new GetUsersResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.PasswordHash,
                    Role = user.Role,
                    Basket = user.Basket,
                };
                response.Add(result);
            }
            return response;
        }
    }
}
