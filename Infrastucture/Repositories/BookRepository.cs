using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _appDbContext.Books.AddAsync(entity);
        }

        public async void Delete(string name)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Title == name);
            if (book != null)
                _appDbContext.Books.Remove(book);
        }

        public async void Delete(Guid Id)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Id == Id);
            if (book != null)
                _appDbContext.Books.Remove(book);
        }

        public Task<List<Book>> GetAllAsync()
        {
            return _appDbContext.Books.AsNoTracking().ToListAsync();
        }

        public void Update(Book entity)
        {
            _appDbContext.Books.Update(entity);
        }
    }
}
