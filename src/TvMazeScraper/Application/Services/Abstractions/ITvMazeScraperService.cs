using System.Threading.Tasks;

namespace TvMazeScraper.Application.Services.Abstractions
{
    public interface ITvMazeScraperService
    {
        void ScrapeAsync(object state);
    }
}
