using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Abstractions;
using TvMazeScraper.Domain.SeedWork;
using TvMazeScraper.Infrastructure.Persistence.DB.Abstractions;

namespace TvMazeScraper.Domain.Aggregates
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ITvMazeScraperDatabase database;

        protected Repository(ITvMazeScraperDatabase database)
        {
            this.database = database;
        }

        protected void CreateAscendingIndex(bool isUnique, params Expression<Func<T, object>>[] expressions)
        {
            try
            {
                var indexKeysDefinition = Builders<T>
                    .IndexKeys
                    .Combine(
                        expressions.Select(
                                expression =>
                                    Builders<T>
                                        .IndexKeys
                                        .Ascending(expression))
                            .ToArray());

                var createIndexOptions = new CreateIndexOptions { Unique = isUnique };

                this.Collection.Indexes.CreateOne(new CreateIndexModel<T>(indexKeysDefinition, createIndexOptions));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected IMongoCollection<T> Collection => this.database.GetCollection<T>();

        protected FilterDefinitionBuilder<T> FilterBuilder => Builders<T>.Filter;

        protected SortDefinitionBuilder<T> SortBuilder => Builders<T>.Sort;

        public ProjectionDefinitionBuilder<T> ProjectionBuilder => Builders<T>.Projection;

        public IndexKeysDefinitionBuilder<T> IndexKeysBuilder => Builders<T>.IndexKeys;

        public async Task InsertAsync(T entity)
        {
            try
            {
                await this.Collection.InsertOneAsync(entity);
            }
            catch (MongoWriteException mongoWriteException)
            {
                var writeErrorCode = mongoWriteException.WriteError.Code;

                if (writeErrorCode == 11000)
                {
                    throw new Exception(mongoWriteException.Message);
                }

                throw;
            }
        }

        public async Task InsertManyAsync(IEnumerable<T> entities)
        {
            try
            {
                await this.Collection.InsertManyAsync(entities);
            }
            catch (MongoWriteException mongoWriteException)
            {
                var writeErrorCode = mongoWriteException.WriteError.Code;

                if (writeErrorCode == 11000)
                {
                    throw new Exception(mongoWriteException.Message);
                }

                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await this.Collection.Find(filter).ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await this.Collection.CountDocumentsAsync(filter);
        }
    }
}
