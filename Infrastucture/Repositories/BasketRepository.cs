using Domain.Entities;
using Domain.Repositories;

namespace Infrastucture.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task CreateAsync(Basket entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Basket>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Basket entity)
        {
            throw new NotImplementedException();
        }
    }
}
