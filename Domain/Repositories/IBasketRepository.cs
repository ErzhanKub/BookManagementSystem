using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Book>> GetAllBooksFromBasketAsync();
        Task<Book> AddBookToBasketAsync(string bookTitle);
        Task<bool> RemoveBookFromBasketAsync(string bookTitle);
        Task<decimal> Checkout();
    }
}
