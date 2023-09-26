using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.Delete
{
    /// <summary>
    /// Class representing a command to delete users by their IDs.
    /// </summary>
    public record DeleteUsersByIdCommand : IRequest<bool>
    {
        /// <summary>
        /// The IDs of the users to be deleted.
        /// </summary>
        public required Guid[] Id { get; init; }
    }

    /// <summary>
    /// Handler for the command to delete users by their IDs.
    /// </summary>
    internal class DeleteUsersByIdHandler : IRequestHandler<DeleteUsersByIdCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeleteUsersByIdHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete users by their IDs.
        /// </summary>
        /// <param name="command">The command to delete users.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the users were deleted successfully, false otherwise.</returns>
        public async Task<bool> Handle(DeleteUsersByIdCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(command.Id).ConfigureAwait(false);
            if (result == true)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }

}
