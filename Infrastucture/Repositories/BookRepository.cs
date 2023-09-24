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

        public async Task<bool> DeleteAsync(params string[] names)
        {
            var books = await _appDbContext.Books.Where(b => names.Contains(b.Title)).ToListAsync().ConfigureAwait(false);
            if (books.Any())
            {
                _appDbContext.Books.RemoveRange(books);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(params Guid[] Id)
        {
            var books = await _appDbContext.Books.Where(b => Id.Contains(b.Id)).ToListAsync().ConfigureAwait(false);
            if (books.Any())
            {
                _appDbContext.Books.RemoveRange(books);
                return true;
            }
            return false;
        }

        public Task<List<Book>> GetAllAsync()
        {
            return _appDbContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByTitle(string title)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Title == title);
            return book ?? default;
        }


        public void Update(Book entity)
        {
            _appDbContext.Books.Update(entity);
        }
    }
}
