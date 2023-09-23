using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User?> CheckUserCredentials(string username, string password)
        {
            var userExists = await _appDbContext.Users.AnyAsync(u => u.Username == username).ConfigureAwait(false);
            if (!userExists)
                return null;
            var user = await _appDbContext.Users.FirstAsync(u => u.Username == username).ConfigureAwait(false);
            if (await HashPasswordAsync(password).ConfigureAwait(false) != user.PasswordHash)
                return null;
            return user;
        }

        public async Task CreateAsync(User user)
        {
            var hash = await HashPasswordAsync(user.PasswordHash).ConfigureAwait(false);
            user.PasswordHash = hash;
            await _appDbContext.Users.AddAsync(user).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == name).ConfigureAwait(false);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == Id).ConfigureAwait(false);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _appDbContext.Users.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<User?> GetByName(string name)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == name).ConfigureAwait(false);
            return user;
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return await Task.FromResult(hash).ConfigureAwait(false);
        }

        public void Update(User entity)
        {
            _appDbContext.Users.Update(entity);
        }
    }
}
