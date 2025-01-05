using Task2Chat.Core.Repositories;

namespace Task2Chat.Infrastructure.UnitOfWork
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : class;
        Task SaveChangesAsync();
    }
}
