using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions
{
    public interface IShowCastTranslator
    {
        Task<IEnumerable<ShowCast>> ToShowCast(HttpResponseMessage httpResponse);
    }
}
