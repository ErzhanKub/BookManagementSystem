using Application.Shared;
using Application.Users.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;


namespace Application.Users.Commands
{
    public record CreateUserCommand : IRequest<UserDto>
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
        public required Role Role { get; init; }
    }

    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.Password,
                Role = request.Role,
                Basket = new(),
            };

            await _userRepository.CreateAsync(user).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            var response = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
            };
            return response;
        }
    }
}
