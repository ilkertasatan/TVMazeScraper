using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions
{
    public interface IShowCastAnticorruption
    {
        Task<IEnumerable<ShowCast>> GetCastByShowId(int showId);
    }
}
