using Task2Chat.Core.Repositories;
using Task2Chat.Infrastructure.Data;

namespace Task2Chat.Infrastructure.UnitOfWork
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly Dictionary<Type, object> _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : class
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<,>).MakeGenericType(type, typeof(TKey));
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories[type] = repositoryInstance;
            }

            return (IGenericRepository<T, TKey>)_repositories[type];
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
