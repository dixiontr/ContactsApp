using System.Linq.Expressions;
using ContactsApp.Core.Interfaces.Entity;
using ContactsApp.Core.Interfaces.Repository;
using MongoDB.Driver;

namespace ContactsApp.ReportService.UnitOfWork.Repositories
{

    public class MongoGenericRepository<T> : IRepository<T> where T: IEntity
    {
        private readonly IMongoCollection<T> _dbCollection;
        private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

        public MongoGenericRepository(IMongoDatabase database, string collectionName)
        {
            _dbCollection = database.GetCollection<T>(collectionName);
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filterDefinition = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
            return await _dbCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public  T Remove(T entity)
        {
            FilterDefinition<T> filterDefinition = _filterDefinitionBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            _dbCollection.DeleteOne(filterDefinition);
            return entity;
        }

        public T Update(T entity)
        {
            FilterDefinition<T> filterDefinition = _filterDefinitionBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            _dbCollection.ReplaceOne(filterDefinition, entity);
            return entity;
        }
    }

}