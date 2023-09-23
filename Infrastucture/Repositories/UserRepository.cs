using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(User entity)
        {
            await _appDbContext.Users.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == name);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                return true;
            }
            return false;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _appDbContext.Users.AsNoTracking().ToListAsync();
        }

        public Task<User> GetByName(string name)
        {
            var user = _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == name);
            return user;
        }

        public void Update(User entity)
        {
            _appDbContext.Users.Update(entity);
        }
    }
}
