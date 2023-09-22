using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task CreateAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
