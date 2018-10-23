using Humanizer;
using MongoDB.Driver;
using TvMazeScraper.Infrastructure.Persistence.DB.Abstractions;

namespace TvMazeScraper.Infrastructure.Persistence.DB
{
    public class TvMazeScraperDatabase : ITvMazeScraperDatabase
    {
        private readonly IMongoDatabase mongo;

        public TvMazeScraperDatabase(IMongoDatabase database)
        {
            this.mongo = database;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return this.mongo.GetCollection<T>(typeof(T).Name.Pluralize());
        }
    }
}
