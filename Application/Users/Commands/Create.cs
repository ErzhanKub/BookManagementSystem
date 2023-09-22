using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public record CreateUserResponse
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public List<Role> Roles { get; set; } = new();
        public Basket? Basket { get; set; }
    }
    public record CreateUserCommand : IRequest<CreateUserResponse>
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
        public List<Role> Roles { get; init; } = new();
    }

    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Roles = request.Roles,
            };
            await _userRepository.CreateAsync(user);
            await _unitOfWork.CommitAsync();

            var response = new CreateUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Roles = user.Roles,
                Basket = user.Basket,
            };
            return response;
        }
    }
}
