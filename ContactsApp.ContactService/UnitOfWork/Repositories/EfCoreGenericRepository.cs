using System.Linq.Expressions;
using ContactsApp.Core.Interfaces.Entity;
using ContactsApp.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ContactsApp.ContactService.UnitOfWork.Repositories
{
    public class EfCoreGenericRepository<TEntity> : IEfCoreRepository<TEntity> where TEntity : class, IEntity
    {
        private DbContext _context;
        public EfCoreGenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Where(filter).ToListAsync();
        }
        public Task<List<TResult>> GetAllWithSelectAsync<TResult>(Expression<Func<TEntity, TResult>> filter)
        {
            return _context.Set<TEntity>().Select(filter).ToListAsync();
        }
        public Task<List<TEntity>> GetAllWithIncludeAsync<TProperty>(Expression<Func<TEntity, TProperty>> filter)
        {
            return _context.Set<TEntity>().Include(filter).ToListAsync();
        }

        public Task<TEntity> GetAsync(Guid id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public Task<TResult> GetWithSelectAsync<TResult>(Expression<Func<TEntity, TResult>> filter)
        {
            return _context.Set<TEntity>().Select(filter).FirstOrDefaultAsync();
        }
        
        public Task<TEntity> GetWithIncludeAsync<TProperty>(Expression<Func<TEntity, TProperty>> filter)
        {
            return _context.Set<TEntity>().Include(filter).FirstOrDefaultAsync();
        }

        public Task<TEntity> GetWithIncludeAsync<TProperty>(Guid id, Expression<Func<TEntity, TProperty>> filter)
        {
            return _context.Set<TEntity>().Where(x => x.Id.Equals(id)).Include(filter).FirstOrDefaultAsync();
        }

        public TEntity Remove(TEntity entity)
        {
            return _context.Set<TEntity>().Remove(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _context.Set<TEntity>().Update(entity).Entity;
        }
    }

}