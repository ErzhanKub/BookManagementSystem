using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public record DeleteUserByNameCommand : IRequest<bool>
    {
        public required string Username { get; init; }
    }

    internal class DeleteUserByNameHandler : IRequestHandler<DeleteUserByNameCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserByNameHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserByNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(request.Username);
            if (result == true)
                await _unitOfWork.CommitAsync();
            return result;
        }
    }
}
