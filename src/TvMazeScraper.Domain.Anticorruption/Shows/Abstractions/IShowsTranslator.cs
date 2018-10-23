using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.Shows.Abstractions
{
    public interface IShowsTranslator
    {
        Task<IEnumerable<Show>> ToShows(HttpResponseMessage httpResponse);
    }
}
