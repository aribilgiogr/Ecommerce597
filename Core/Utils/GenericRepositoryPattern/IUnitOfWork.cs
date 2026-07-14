using Core.Abstracts.Bases;

namespace Core.Utils.GenericRepositoryPattern
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CommitAsync();
    }
}
