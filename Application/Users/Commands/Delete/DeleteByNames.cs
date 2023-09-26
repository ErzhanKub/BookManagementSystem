using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public record DeleteUsersByNamesCommand : IRequest<bool>
    {
        public required string[] Usernames { get; init; }
    }

    internal class DeleteUsersByNamesHandler : IRequestHandler<DeleteUsersByNamesCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsersByNamesHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUsersByNamesCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(command.Usernames).ConfigureAwait(false);
            if (result == true)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }
}
