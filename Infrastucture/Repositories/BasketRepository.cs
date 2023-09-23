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

        public Task<Book> AddBookToBasketAsync(string bookTitle)
        {
            //var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Title == bookTitle);
            //if (book == null) return null;
            //var basket = new Basket
            //{
            //    Books = new List<Book> { book }
            //};
            //await _appDbContext.Baskets.AddAsync(basket);
            throw new NotImplementedException();

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
