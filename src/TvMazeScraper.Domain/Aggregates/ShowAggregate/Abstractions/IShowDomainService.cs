using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions
{
    public interface IShowDomainService
    {
        Task<IEnumerable<Show>> GetShows(int offset, int limit);
    }
}
