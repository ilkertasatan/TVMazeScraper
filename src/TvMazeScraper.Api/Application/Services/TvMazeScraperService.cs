using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Api.Application.DTO;
using TvMazeScraper.Api.Application.Services.Abstractions;
using TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions;

namespace TvMazeScraper.Api.Application.Services
{
    public class TvMazeScraperService : ITvMazeScraperService
    {
        private readonly IShowDomainService showDomainService;

        public TvMazeScraperService(IShowDomainService showDomainService)
        {
            this.showDomainService = showDomainService;
        }

        public async Task<IEnumerable<ShowDTO>> GetShows(int offset, int limit)
        {
            IList<ShowDTO> showsDTO = new List<ShowDTO>();

            var shows = await this.showDomainService.GetShows(offset, limit);

            foreach (var show in shows)
            {
                var showDTO = new ShowDTO
                {
                    Id = show.ExternalId,
                    Name = show.Name
                };

                foreach (var actor in show.Actors)
                {
                    showDTO.Cast.Add(new ShowCastDTO
                    {
                        Id = actor.Id,
                        Name = actor.Name,
                        BirthDay = actor.BirthDay
                    });
                }

                showsDTO.Add(showDTO);
            }

            return showsDTO;
        }
    }
}