﻿using Application.Shared;
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
    public record CreateUserResponse
    {
        public Guid Id { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public required Role Role { get; init; }
        public Basket? Basket { get; init; }
    }
    public record CreateUserCommand : IRequest<CreateUserResponse>
    {
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public required Role Role { get; init; }
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
                PasswordHash = request.Password,
                Role = request.Role,
            };
            await _userRepository.CreateAsync(user);
            await _unitOfWork.CommitAsync();

            var response = new CreateUserResponse
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