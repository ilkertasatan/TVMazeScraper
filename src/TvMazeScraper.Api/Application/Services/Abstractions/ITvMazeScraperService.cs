using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Api.Application.DTO;

namespace TvMazeScraper.Api.Application.Services.Abstractions
{
    public interface ITvMazeScraperService
    {
        Task<IEnumerable<ShowDTO>> GetShows(int offset, int limit);
    }
}
