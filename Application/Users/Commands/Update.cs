using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public record UpdateUserResponse
    {
        public Guid Id { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public required Role Role { get; init; }
        public Basket? Basket { get; init; }
    }
    public record UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public required Role Role { get; init; }
        public Basket? Basket { get; init; }
    }

    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByName(request.Username);
            if (user != null)
            {
                user.Username = request.Username;
                user.PasswordHash = request.PasswordHash;
                user.Role = request.Role;
                user.Basket = request.Basket;

                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
            }

            var response = new UpdateUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                Basket = user.Basket
            };

            return response;
        }
    }
}
