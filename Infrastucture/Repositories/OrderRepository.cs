using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    /// <summary>
    /// Provides methods for managing orders in a database.
    /// Implements the IOrderRepository interface.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Creates a new order in the database.
        /// </summary>
        public async Task Create(Order order)
        {
            await _appDbContext.Orders.AddAsync(order);
        }

        /// <summary>
        /// Deletes an order from the database by its ID.
        /// </summary>
        public async Task<bool> Delete(Guid id)
        {
            var order = await _appDbContext.Orders.FindAsync(id).ConfigureAwait(false);
            if (order != null)
            {
                _appDbContext.Orders.Remove(order);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _appDbContext.Orders.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves an order by its ID from the database.
        /// </summary>
        public async Task<Order?> GetById(Guid id)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);
            return order;
        }

        /// <summary>
        /// Retrieves all orders for a specific user from the database.
        /// </summary>
        public async Task<IEnumerable<Order>> GetByUsername(string username)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            if (user is null) return Enumerable.Empty<Order>();
            var orders = await _appDbContext.Orders.Where(o => o.UserId == user.Id).ToListAsync().ConfigureAwait(false);
            return orders;
        }

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        public void Update(Order order)
        {
            _appDbContext.Orders.Update(order);
        }
    }

}
