using Application.Shared;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.Delete
{
    public record DeleteBookByIdCommand : IRequest<bool>
    {
        public required Guid Id { get; init; }
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
