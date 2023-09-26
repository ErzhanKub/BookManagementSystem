using Application.Shared;
using Application.Users.Dtos;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands
{
    /// <summary>
    /// Class representing a command to update a user.
    /// </summary>
    public record UpdateUserCommand : IRequest<UserDto?>
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; init; }

        /// <summary>
        /// The role of the user.
        /// </summary>
        public required Role Role { get; init; }
    }

    /// <summary>
    /// Handler for the command to update a user.
    /// </summary>
    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to update a user.
        /// </summary>
        /// <param name="command">The command to update a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated user data transfer object or null.</returns>
        public async Task<UserDto?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(command.Username).ConfigureAwait(false);
            if (user is not null)
            {
                user.Username ??= command.Username;
                user.PasswordHash ??= command.Password;
                user.Role = command.Role;

                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();

                var response = new UserDto
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
