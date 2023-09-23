using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(Book entity)
        {
            await _appDbContext.Books.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Title == name).ConfigureAwait(false);
            if (book != null)
            {
                _appDbContext.Books.Remove(book);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Id == Id).ConfigureAwait(false);
            if (book != null)
            {
                _appDbContext.Books.Remove(book);
                return true;
            }
            return false;
        }

        public Task<List<Book>> GetAllAsync()
        {
            return _appDbContext.Books.AsNoTracking().ToListAsync();
        }

        public Task<Book?> GetByTitle(string title)
        {
            var book = _appDbContext.Books.FirstOrDefaultAsync(b => b.Title == title);
            return book;
        }

        public void Update(Book entity)
        {
            _appDbContext.Books.Update(entity);
        }
    }
}
