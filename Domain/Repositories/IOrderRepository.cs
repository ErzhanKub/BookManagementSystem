using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Defines the operations for managing orders in a repository.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Updates an existing order in the repository.
        /// </summary>
        void Update(Order order);

        /// <summary>
        /// Deletes an order from the repository by its ID.
        /// </summary>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// Retrieves all orders from the repository.
        /// </summary>
        Task<IEnumerable<Order>> GetAll();

        /// <summary>
        /// Retrieves all orders for a specific user from the repository.
        /// </summary>
        Task<IEnumerable<Order>> GetByUsername(string username);

        /// <summary>
        /// Retrieves an order by its ID from the repository.
        /// </summary>
        Task<Order?> GetById(Guid id);

        /// <summary>
        /// Creates a new order in the repository.
        /// </summary>
        Task Create(Order order);
    }

}
