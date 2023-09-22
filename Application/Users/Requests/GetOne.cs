using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Requests
{
    public record GetUserResponse
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; init; }
        public Basket? Basket { get; set; }
    }
    public record GetUserByNameCommand : IRequest<GetUserResponse> { public required string Username { get; init; } }
    internal class GetUserByNameHandler : IRequestHandler<GetUserByNameCommand, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByName(request.Username);
            var response = new GetUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.PasswordHash,
                Role = user.Role,
                Basket = user.Basket,
            };
            return response;
        }
    }
}
