using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Abstractions;

namespace TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions
{
    public interface IShowRepository : IRepository<Show>
    {
        Task<Show> FindLastShow();
        Task<List<Show>> FindShows(int offset, int limit);
    }
}
