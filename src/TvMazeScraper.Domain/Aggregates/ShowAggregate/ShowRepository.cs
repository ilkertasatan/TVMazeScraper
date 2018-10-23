using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions;
using TvMazeScraper.Infrastructure.Persistence.DB.Abstractions;

namespace TvMazeScraper.Domain.Aggregates.ShowAggregate
{
    public class ShowRepository : Repository<Show>, IShowRepository
    {
        public ShowRepository(ITvMazeScraperDatabase database) : base(database)
        {
            this.CreateAscendingIndex(true, t => t.ExternalId);
        }

        public Task<Show> FindLastShow()
        {
            var sort = SortBuilder.Descending(x => x.ExternalId);

            return this.Collection
                .Find(x => true)
                .Limit(1)
                .Sort(sort)
                .FirstOrDefaultAsync();
        }

        public Task<List<Show>> FindShows(int offset, int limit)
        {
            var sort = SortBuilder.Ascending(x => x.ExternalId);

            return this.Collection
                .Find(x => true)
                .Limit(limit)
                .Skip(offset * limit)
                .Sort(sort)
                .ToListAsync();
        }
    }
}
