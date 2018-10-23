using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Anticorruption.Abstractions;
using TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption.ShowCast
{
    public class ShowCastAnticorruption : IShowCastAnticorruption
    {
        private readonly ITvMazeRequestBuilder requestBuilder;
        private readonly IShowCastTranslator translator;

        public ShowCastAnticorruption(ITvMazeRequestBuilder requestBuilder, IShowCastTranslator translator)
        {
            this.requestBuilder = requestBuilder;
            this.translator = translator;
        }

        public async Task<IEnumerable<ShowCast>> GetCastByShowId(int showId)
        {
            var httpResponse = await requestBuilder.GetCastByShowId(showId);
            return await translator.ToShowCast(httpResponse);
        }
    }
}
