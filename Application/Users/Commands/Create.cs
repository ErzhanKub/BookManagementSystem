using Application.Shared;
using Application.Users.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;


namespace Application.Users.Commands
{
    /// <summary>
    /// Class representing a command to create a user.
    /// </summary>
    public record CreateUserCommand : IRequest<UserDto>
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
    /// Handler for the command to create a user.
    /// </summary>
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to create a user.
        /// </summary>
        /// <param name="request">The command to create a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created user data transfer object.</returns>
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
