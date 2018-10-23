using MongoDB.Driver;

namespace TvMazeScraper.Infrastructure.Persistence.DB.Abstractions
{
    public interface ITvMazeScraperDatabase
    {
        IMongoCollection<T> GetCollection<T>();
    }
}
