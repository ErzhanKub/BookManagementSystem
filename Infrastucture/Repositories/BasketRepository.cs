using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDbContext _appDbContext;

        public BasketRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AddBookToBasketAsync(string bookTitle, Basket basket)
        {
            var hasBook = await _appDbContext.Books.AnyAsync(x => x.Title == bookTitle);
            if (hasBook)
            {
                basket.Books ??= new List<Book>();
                var book = await _appDbContext.Books.FirstAsync(x => x.Title == bookTitle);
                basket.Books.Add(book);
                await _appDbContext.Baskets.AddAsync(basket);
                return true;
            }
            return false;
        }


        public Task<decimal> Checkout()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksFromBasketAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBookFromBasketAsync(string bookTitle)
        {
            throw new NotImplementedException();
        }
    }
}
