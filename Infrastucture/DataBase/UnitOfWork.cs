using Application.Shared;
using Infrastucture.DataBase;

namespace Infrastructure.DataBase
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
