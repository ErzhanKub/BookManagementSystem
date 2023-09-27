using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        void Update(Order order);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Order>> GetAll();
        Task<Order?> GetByUsername(string username);
        Task<Order?> GetById(Guid id);
        Task Create(Order order);
    }
}
