using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Net;
using System.Threading.Tasks;
using TvMazeScraper.Api.Application.Services.Abstractions;

namespace TvMazeScraper.Api.Controllers
{
    [Route("api/shows")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        private readonly ITvMazeScraperService service;

        public TvShowController(ITvMazeScraperService service)
        {
            this.service = service;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetShows([FromQuery]int offset = 0, [FromQuery]int limit = 100)
        {
            var shows = await this.service.GetShows(offset, limit);
            return Ok(shows);
        }
    }
}