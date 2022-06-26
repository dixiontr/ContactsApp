using System.Linq.Expressions;
using ContactsApp.Core.Interfaces.Entity;
using ContactsApp.Core.Interfaces.Repository;

namespace ContactsApp.ContactService.UnitOfWork.Repositories
{

    public interface IEfCoreRepository<T> : IRepository<T> where T : IEntity
    {
        Task<List<TResult>> GetAllWithSelectAsync<TResult>(Expression<Func<T, TResult>> filter);
        Task<List<T>> GetAllWithIncludeAsync<TProperty>(Expression<Func<T, TProperty>> filter);
        Task<TResult> GetWithSelectAsync<TResult>(Expression<Func<T, TResult>> filter);
        Task<T> GetWithIncludeAsync<TProperty>(Expression<Func<T, TProperty>> filter);
        Task<T> GetWithIncludeAsync<TProperty>(Guid id,Expression<Func<T, TProperty>> filter);
    }

}