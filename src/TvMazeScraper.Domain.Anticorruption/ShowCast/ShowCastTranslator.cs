using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption.ShowCast
{
    public class ShowCastTranslator : IShowCastTranslator
    {
        public async Task<IEnumerable<ShowCast>> ToShowCast(HttpResponseMessage httpResponse)
        {
            var content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ShowCast>>(content);
        }
    }
}