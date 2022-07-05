using MongoDB.Driver;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharp.Mongo.Connection{
    public class Collection2<T> where T : class{

        private IMongoCollection<T> mongoCollection;
        protected readonly IMongoClient _mongoClient;
        public IMongoDatabase _mongoDatabase;
        public Collection2(IMongoClient mongoClient, string databaseName, string CollectionName = ""){
            this._mongoClient = mongoClient;
            this._mongoDatabase = mongoClient.GetDatabase(databaseName);

            if(string.IsNullOrEmpty(CollectionName))
                CollectionName = typeof(T).Name;
            
            mongoCollection = this._mongoDatabase.GetCollection<T>(CollectionName);
        }
        
        public virtual IQueryable<T> All()
        {
            return mongoCollection.AsQueryable();
        }

        public virtual T Single(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return Where(expression).Single();
        }
        public virtual T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return Where(expression).SingleOrDefault();
        }
        public virtual IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return All().Where(expression);
        }
        public virtual async Task<IAsyncCursor<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await mongoCollection.FindAsync(expression);
        }

        public virtual async Task<IAsyncCursor<T>> FindAsync(FilterDefinition<T> filter)
        {
            return await mongoCollection.FindAsync(filter);
        }

        public virtual async Task<bool> DeleteAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var result = await mongoCollection.DeleteOneAsync(predicate);
            return result.IsAcknowledged;
        }
        public virtual async Task<bool> DeleteAsync(FilterDefinition<T> filter)
        {
            var result = await mongoCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }
        public virtual async Task<T> ReplaceAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, T item)
        {
            await mongoCollection.ReplaceOneAsync(predicate, item);
            return (item);
        }
        public virtual async Task<T> ReplaceAsync(FilterDefinition<T> filter, T item)
        {
            await mongoCollection.ReplaceOneAsync(filter, item);
            return item;
        }
        public virtual async Task<T> AddAsync(T item)
        {
            await mongoCollection.InsertOneAsync(item);
            return item;
        }
        public virtual void AddRangeAsync(IEnumerable<T> items)
        {
            mongoCollection.InsertManyAsync(items);
        }
        public virtual void AddWithTransactionAsync(IClientSessionHandle session, T item)
        {
            mongoCollection.InsertOneAsync(session, item);
        }
        public virtual async Task<T> UpdateAsync(FilterDefinition<T> filter, T item)
        {
            await mongoCollection.ReplaceOneAsync(filter, item);
            return item;
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter,UpdateDefinition<T> updateDefinition,UpdateOptions options)
        {
          return  await mongoCollection.UpdateOneAsync(filter, updateDefinition, options);
        }

        public virtual  UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, UpdateOptions options)
        {
            return  mongoCollection.UpdateOne(filter, updateDefinition, options);
        }
        public virtual void UpdateWithTransactionAsync(IClientSessionHandle session, FilterDefinition<T> filter, T item)
        {
            mongoCollection.ReplaceOneAsync(session, filter, item);
        }
        public virtual async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> item)
        {
            return await mongoCollection.FindOneAndUpdateAsync(filter, item);
        }

        public virtual async Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> item)
        {
            return await mongoCollection.UpdateManyAsync(filter, item);
        }

        public virtual async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> item, FindOneAndUpdateOptions<T, T> options)
        {
            return await mongoCollection.FindOneAndUpdateAsync(filter, item, options);
        }
    }

}