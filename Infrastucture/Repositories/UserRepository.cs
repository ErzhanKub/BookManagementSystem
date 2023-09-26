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

        /// <summary>
        /// Checks if the user credentials are valid.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>The user object if the credentials are valid; otherwise, null.</returns>
        public async Task<User?> CheckUserCredentialsAsync(string username, string password)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            if (user is null || await HashPasswordAsync(password).ConfigureAwait(false) != user.PasswordHash)
                return null;
            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        public async Task CreateAsync(User user)
        {
            var hash = await HashPasswordAsync(user.PasswordHash).ConfigureAwait(false);
            user.PasswordHash = hash;
            await _appDbContext.Users.AddAsync(user).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes users with matching usernames.
        /// </summary>
        /// <param name="names">The usernames to delete.</param>
        /// <returns>True if any users were deleted; otherwise, false.</returns>
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
        /// <summary>
        /// Deletes users by their IDs.
        /// </summary>
        /// <param name="Id">The IDs of the users to delete.</param>
        /// <returns><c>true</c> if the users were deleted; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<User>> GetAllAsync()
        {
            return await _appDbContext.Users.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of users by their names.
        /// </summary>
        /// <param name="names">The names of the users to retrieve.</param>
        /// <returns>A list of users.</returns>
        public async Task<IEnumerable<User>> GetSomeUsersByNamesAsync(params string[] names)
        {
            var users = await _appDbContext.Users.Where(u => names.Contains(u.Username)).ToListAsync().ConfigureAwait(false);
            return users;
        }

        /// <summary>
        /// Gets a user by their name.
        /// </summary>
        /// <param name="username">The name of the user to retrieve.</param>
        /// <returns>The user with the specified name, or null if not found.</returns>
        public async Task<User?> GetUserByNameAsync(string username)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            return user ?? null;
        }

        /// <summary>
        /// Hashes a password using SHA256.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
        public async Task<string> HashPasswordAsync(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return await Task.FromResult(hash).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="entity">The user to update.</param>
        public void Update(User entity)
        {
            _appDbContext.Users.Update(entity);
        }

    }
}
