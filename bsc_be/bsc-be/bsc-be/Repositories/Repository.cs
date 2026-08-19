using bsc_be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace bsc_be.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BscDbContext _context;
        private readonly DbSet<T> _dbSet;
        private IDbContextTransaction? _transaction;
        public Repository(BscDbContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
