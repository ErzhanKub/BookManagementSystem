using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands
{
    public record UpdateUserResponse
    {
        public Guid Id { get; init; }
        public required string Username { get; init; }
        public required Role Role { get; init; }
    }
    public record UpdateUserCommand : IRequest<UpdateUserResponse?>
    {
        public required string Username { get; set; }
        public required string Password { get; init; }
        public required Role Role { get; init; }
    }

    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserResponse?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(request.Username);
            if (user is not null)
            {
                user.Username ??= request.Username;
                user.PasswordHash ??= request.Password;
                user.Role = request.Role;

                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();

                var response = new UpdateUserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                };
                return response;
            }
            return default;
        }

    }
}
