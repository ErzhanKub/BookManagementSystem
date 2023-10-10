using Application.Shared;
using Domain.Repositories;

namespace Application.Users.Commands.Delete
{
    /// <summary>
    /// Class representing a command to delete users by their usernames.
    /// </summary>
    public record DeleteUsersByNamesCommand : IRequest<bool>
    {
        /// <summary>
        /// The usernames of the users to be deleted.
        /// </summary>
        public required string[] Usernames { get; init; }
    }

    /// <summary>
    /// Handler for the command to delete users by their usernames.
    /// </summary>
    internal class DeleteUsersByNamesHandler : IRequestHandler<DeleteUsersByNamesCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeleteUsersByNamesHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete users by their usernames.
        /// </summary>
        /// <param name="command">The command to delete users.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the users were deleted successfully, false otherwise.</returns>
        public async Task<bool> Handle(DeleteUsersByNamesCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(command.Usernames).ConfigureAwait(false);
            if (result == true)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }

}
