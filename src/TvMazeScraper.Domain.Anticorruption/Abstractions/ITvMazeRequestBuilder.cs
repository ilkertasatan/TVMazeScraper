using System.Net.Http;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.Abstractions
{
    public interface ITvMazeRequestBuilder
    {
        Task<HttpResponseMessage> GetAllShows(int pageIndex);
        Task<HttpResponseMessage> GetCastByShowId(int showId);
    }
}
