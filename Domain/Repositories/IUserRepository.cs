using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string name);
    }
}
