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

        public async Task<User?> CheckUserCredentialsAsync(string username, string password)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            if (user is null || await HashPasswordAsync(password).ConfigureAwait(false) != user.PasswordHash)
                return null;
            return user;
        }

        public async Task CreateAsync(User user)
        {
            var hash = await HashPasswordAsync(user.PasswordHash).ConfigureAwait(false);
            user.PasswordHash = hash;
            await _appDbContext.Users.AddAsync(user).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(params string[] names)
        {
            var users = await _appDbContext.Users.Where(u => names.Contains(u.Username)).ToListAsync().ConfigureAwait(false);
            if (users.Any())
            {
                _appDbContext.Users.RemoveRange(users);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(params Guid[] Id)
        {
            var users = await _appDbContext.Users.Where(u => Id.Contains(u.Id)).ToListAsync().ConfigureAwait(false);
            if (users.Any())
            {
                _appDbContext.Users.RemoveRange(users);
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _appDbContext.Users.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetSomeUsersByNamesAsync(params string[] names)
        {
            var users = await _appDbContext.Users.Where(u => names.Contains(u.Username)).ToListAsync().ConfigureAwait(false);
            return users;
        }

        public async Task<User?> GetUserByNameAsync(string username)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            return user ?? null;
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
