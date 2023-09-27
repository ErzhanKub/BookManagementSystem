using Application.Books.Dtos;
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

        /// <summary>
        /// Creates a new book asynchronously.
        /// </summary>
        /// <param name="entity">The book entity to create.</param>
        public async Task CreateAsync(Book entity)
        {
            await _appDbContext.Books.AddAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes books asynchronously by title.
        /// </summary>
        /// <param name="title">The titles of the books to delete.</param>
        /// <returns><c>true</c> if any books were deleted; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteAsync(params string[] title)
        {
            var books = await _appDbContext.Books.Where(b => title.Contains(b.Title)).ToListAsync().ConfigureAwait(false);
            if (books.Any())
            {
                _appDbContext.Books.RemoveRange(books);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes books asynchronously by ID.
        /// </summary>
        /// <param name="Id">The IDs of the books to delete.</param>
        /// <returns><c>true</c> if any books were deleted; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Gets all books asynchronously.
        /// </summary>
        public Task<List<Book>> GetAllAsync()
        {
            return _appDbContext.Books.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets a book by title asynchronously.
        /// </summary>
        /// <param name="title">The title of the book to get.</param>
        public async Task<Book?> GetByTitleAsync(string title)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Title == title);
            return book ?? default;
        }

        /// <summary>
        /// Gets some books by title asynchronously.
        /// </summary>
        /// <param name="titles">The titles of the books to get.</param>
        public async Task<IEnumerable<Book>> GetSomeByTitleAsync(params string[] titles)
        {
            var books = await _appDbContext.Books.Where(b => titles.Contains(b.Title)).ToListAsync().ConfigureAwait(false);
            return books;
        }


        /// <summary>
        /// Updates a book.
        /// </summary>
        public void Update(Book entity)
        {
            _appDbContext.Books.Update(entity);
        }
    }

}
