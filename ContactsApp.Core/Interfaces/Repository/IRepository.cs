using System.Linq.Expressions;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.Core.Interfaces.Repository
{
    public interface IRepository<T>  where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T,bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T,bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}