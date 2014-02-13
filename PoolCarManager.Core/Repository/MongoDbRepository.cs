namespace PoolCarManager.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private MongoDatabase database;
        private MongoCollection<TEntity> collection;

        public MongoDbRepository()
        {
            this.GetDatabase();
            this.GetCollection();
        }

        public bool Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            return this.collection.Insert(entity).Ok;   
        }

        public bool Update(TEntity entity)
        {
            return this.collection.Save(entity).DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity)
        {
            return this.collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return this.collection.AsQueryable().Where(predicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return this.collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return this.collection.FindOneByIdAs<TEntity>(id);
        }

        public TEntity GetByAggregateId(Guid id)
        {
            return this.collection.AsQueryable().Single(m => m.AggregateId == id);
        }

        private void GetDatabase()
        {
            var client = new MongoClient(this.GetConnectionString());
            var server = client.GetServer();

            this.database = server.GetDatabase(this.GetDatabaseName());
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbConnectionString").Replace("{DB_NAME}", this.GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbDatabaseName");
        }

        private void GetCollection()
        {
            this.collection = this.database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
