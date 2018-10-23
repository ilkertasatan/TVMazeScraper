using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions;

namespace TvMazeScraper.Domain.Aggregates.ShowAggregate
{
    public class ShowDomainService : IShowDomainService
    {
        private readonly IShowRepository repository;

        public ShowDomainService(IShowRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Show>> GetShows(int offset, int limit)
        {
            return await this.repository.FindShows(offset, limit);
        }
    }
}
