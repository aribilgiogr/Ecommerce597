using Core.Abstracts.Bases;
using System.Linq.Expressions;

namespace Core.Utils.GenericRepositoryPattern
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>>? where = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>>? where = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? where = null);
    }
}
