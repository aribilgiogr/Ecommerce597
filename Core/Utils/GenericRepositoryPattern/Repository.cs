using Core.Abstracts.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Utils.GenericRepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? where = null)
        {
            // (x => true): Tük kayıtlar anlamına gelir.
            return await _set.AnyAsync(where ?? (x => true));
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? where = null)
        {
            return await _set.CountAsync(where ?? (x => true));
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>>? where = null)
        {
            return await _set.Where(where ?? (x => true)).ToListAsync();
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }
    }
}
