using Domain.Entities;

namespace Domain.Shared
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        Task<bool> DeleteAsync(params string[] names);
        Task<bool> DeleteAsync(params Guid[] Id);
    }
}
