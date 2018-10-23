using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Anticorruption.Abstractions;
using TvMazeScraper.Domain.Anticorruption.Shows.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption.Shows
{
    public class ShowsAnticorruption : IShowsAnticorruption
    {
        private readonly ITvMazeRequestBuilder requestBuilder;
        private readonly IShowsTranslator translator;
        
        public ShowsAnticorruption(ITvMazeRequestBuilder requestBuilder, IShowsTranslator translator)
        {
            this.requestBuilder = requestBuilder;
            this.translator = translator;
        }

        public async Task<IEnumerable<Show>> GetAllShows(int pageIndex)
        {
            var httpResponse = await requestBuilder.GetAllShows(pageIndex);
            return await translator.ToShows(httpResponse);
        }
    }
}