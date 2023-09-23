using Application.Shared;
using Domain.Repositories;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Baskets.Commands
{

    public record AddBookToBasketCommand : IRequest<bool>
    {
        public required string Title { get; init; }
        public required string Username { get; init; }
    }

    internal class AddBookToBasketHandler : IRequestHandler<AddBookToBasketCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public AddBookToBasketHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(AddBookToBasketCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByName(request.Username);
            if (user != null)
            {
                var book = await _bookRepository.GetByTitle(request.Title);
                user.Basket.Books.Add(book);
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
    }
}
