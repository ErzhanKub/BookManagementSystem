using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Book>> GetAllBooksFromBasketAsync();
        Task<bool> AddBookToBasketAsync(string bookTitle, Basket basket);
        Task<bool> RemoveBookFromBasketAsync(string bookTitle);
        Task<decimal> Checkout();
    }
}
