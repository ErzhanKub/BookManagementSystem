using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public record DeleteUsersByIdCommand : IRequest<bool>
    {
        public required Guid[] Id { get; init; }
    }

    internal class DeleteUsersByIdHandler : IRequestHandler<DeleteUsersByIdCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsersByIdHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUsersByIdCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(command.Id).ConfigureAwait(false);
            if (result == true)
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return result;
        }
    }
}
