namespace Application.Shared
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
