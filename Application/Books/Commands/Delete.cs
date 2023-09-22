using Application.Shared;
using Domain.Repositories;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public record DeleteBookByTitleCommand : IRequest<bool>
    {
        public required string Title { get; init; }
    }

    public record DeleteBookByIdCommand : IRequest<bool>
    {
        public required Guid Id { get; init; }
    }

    internal class DeleteBookByTitleHandler : IRequestHandler<DeleteBookByTitleCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookByTitleHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookByTitleCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(request.Title);
            if (result == true)
                await _unitOfWork.CommitAsync();
            return result;
        }
    }

    internal class DeleteBookByIdHandler : IRequestHandler<DeleteBookByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookByIdHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteAsync(request.Id);
            if (result == true)
                await _unitOfWork.CommitAsync();
            return result;
        }
    }
}
