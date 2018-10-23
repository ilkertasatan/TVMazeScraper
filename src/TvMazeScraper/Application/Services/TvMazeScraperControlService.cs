using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvMazeScraper.Application.Services.Abstractions;

namespace TvMazeScraper.Application.Services
{
    public class TvMazeScraperControlService : IHostedService
    {
        private readonly ITvMazeScraperService scraperService;
        private Timer timer;

        public TvMazeScraperControlService(ITvMazeScraperService scraperService)
        {
            this.scraperService = scraperService;
            }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var autoResetEvent = new AutoResetEvent(true);
            timer = new Timer(scraperService.ScrapeAsync, autoResetEvent, 0, (long)TimeSpan.FromHours(12).TotalMilliseconds);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
        }
    }
}
