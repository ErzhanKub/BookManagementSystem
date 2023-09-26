using Application.Shared;
using Infrastucture.DataBase;

namespace Infrastructure.DataBase
{
    /// <summary>
    /// Represents a unit of work pattern implementation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }

}
