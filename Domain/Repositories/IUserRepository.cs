using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> HashPasswordAsync(string password);
        Task<User?> CheckUserCredentials(string username, string password);
        Task<User?> GetByName(string name);
    }
}
