using System.Linq.Expressions;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.Core.Interfaces.Repository
{
    public interface IRepository<T>  where T : IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> filter);
        Task<List<T>> GetAllAsyncWithInclude<TProperty>(Expression<Func<T, TProperty>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T,bool>> filter);
        Task<TResult> GetAsync<TResult>(Expression<Func<T, TResult>> filter);
        Task<T> GetAsyncWithInclude<TProperty>(Expression<Func<T, TProperty>> filter);
        Task<T> GetAsyncWithInclude<TProperty>(Guid id,Expression<Func<T, TProperty>> filter);
        T Remove(T entity);
        T Update(T entity);
    }
}