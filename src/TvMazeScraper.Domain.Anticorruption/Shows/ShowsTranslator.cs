using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Anticorruption.Shows.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption.Shows
{
    public class ShowsTranslator : IShowsTranslator
    {
        public async Task<IEnumerable<Show>> ToShows(HttpResponseMessage httpResponse)
        {
            var content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Show>>(content);
        }
    }
}