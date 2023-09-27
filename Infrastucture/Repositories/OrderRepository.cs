using Domain.Entities;
using Domain.Repositories;
using Infrastucture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(Order order)
        {
            await _appDbContext.Orders.AddAsync(order);
        }

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

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _appDbContext.Orders.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Order?> GetById(Guid id)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);
            return order;
        }

        public async Task<Order?> GetByUsername(string username)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            if (user is null) return null;
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.UserId == user.Id).ConfigureAwait(false);
            return order;
        }


        public void Update(Order order)
        {
            _appDbContext.Orders.Update(order);
        }

    }
}
