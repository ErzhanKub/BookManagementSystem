using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    /// <summary>
    /// Represents a user repository and inherits from the IRepository<User> interface.
    /// Представляет репозиторий пользователей и 
    /// наследуется от интерфейса IRepository<User>.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Hashes the password.
        /// Хэширует пароль.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> HashPasswordAsync(string password);
        /// <summary>
        /// Validates user credentials.
        /// Проверяет учетные данные пользователя.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User?> CheckUserCredentials(string username, string password);
        /// <summary>
        /// Returns a collection of users with the specified names.
        /// Возвращает коллекцию пользователей по указанным именам.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetSomeUsersByNames(params string[] names);
        /// <summary>
        /// Returns the user by the specified name.
        /// Возвращает пользователя по указанному имени.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User?> GetUserByName(string username);
    }
}
