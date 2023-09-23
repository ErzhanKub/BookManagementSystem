using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public record DeleteUserByIdCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }

    internal class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserByIdHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(request.Id);
            if (result == true)
                await _unitOfWork.CommitAsync();
            return result;
        }
    }
}
