namespace bsc_be.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params string[] includeProperties);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
