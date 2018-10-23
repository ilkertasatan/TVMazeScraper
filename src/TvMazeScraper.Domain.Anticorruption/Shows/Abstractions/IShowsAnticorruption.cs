using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.Shows.Abstractions
{
    public interface IShowsAnticorruption
    {
        Task<IEnumerable<Show>> GetAllShows(int pageIndex);
    }
}
