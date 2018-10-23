using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TvMazeScraper.Application.Services.Abstractions;
using TvMazeScraper.Domain.Aggregates.ShowAggregate;
using TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions;
using TvMazeScraper.Domain.Aggregates.ValueObjects;
using TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions;
using TvMazeScraper.Domain.Anticorruption.Shows.Abstractions;

namespace TvMazeScraper.Application.Services
{
    public class TvMazeScraperService : ITvMazeScraperService
    {
        private const int PageSize = 250;

        private readonly IShowsAnticorruption showsAnticorruption;
        private readonly IShowCastAnticorruption showCastAnticorruption;
        private readonly IShowRepository repository;
        private readonly ILogger<TvMazeScraperService> logger;

        public TvMazeScraperService(IShowsAnticorruption showsAnticorruption,
            IShowCastAnticorruption showCastAnticorruption, IShowRepository repository, ILogger<TvMazeScraperService> logger)
        {
            this.showsAnticorruption = showsAnticorruption;
            this.showCastAnticorruption = showCastAnticorruption;
            this.repository = repository;
            this.logger = logger;
        }

        public async void ScrapeAsync(object state)
        {
            try
            {
                var pageIndex = 0;
                var lastShowId = 0;

                var lastShow = await this.repository.FindLastShow();
                if (lastShow != null)
                {
                    pageIndex = CalculatePageIndexFromShowId(lastShow.ExternalId);
                    lastShowId = lastShow.ExternalId;
                }

                this.logger.LogInformation("Collecting TV Shows...");

                while (true)
                {
                    var tvMazeShows = await this.showsAnticorruption.GetAllShows(pageIndex);
                    if (!tvMazeShows.Any())
                    {
                        this.logger.LogInformation("No TV Shows.");
                        return;
                    }

                    var finalTvMazeShows = tvMazeShows.Where(x => x.Id > lastShowId).OrderBy(x => x.Id);
                    IList<Show> showsList = new List<Show>();

                    foreach (var tvMazeShow in finalTvMazeShows)
                    {
                        var show = new Show
                        {
                            ExternalId = tvMazeShow.Id,
                            Name = tvMazeShow.Name
                        };

                        var tvMazeShowCasts = await this.showCastAnticorruption.GetCastByShowId(tvMazeShow.Id);
                        foreach (var tvMazeShowCast in tvMazeShowCasts.OrderByDescending(x => x.Person.BirthDay))
                        {
                            show.Actors.Add(new Actor
                            {
                                Id = tvMazeShowCast.Person.Id,
                                Name = tvMazeShowCast.Person.Name,
                                BirthDay = tvMazeShowCast.Person.BirthDay
                            });
                        }

                        showsList.Add(show);
                    }

                    await this.repository.InsertManyAsync(showsList);
                    this.logger.LogInformation($"Inserted TV Shows. PageIndex: {pageIndex}");

                    var latestShow = tvMazeShows.OrderByDescending(x => x.Id).Take(1).First();
                    pageIndex = CalculatePageIndexFromShowId(latestShow.Id);
                    lastShowId = latestShow.Id;

                    this.logger.LogInformation($"Inserting TV Shows. Next PageIndex: {pageIndex}");
                }
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Catched an exception: {e.Message}");
            }
            finally
            {
                this.logger.LogInformation("Collected TV Shows.");
            }
        }

        private static int CalculatePageIndexFromShowId(long id)
        {
            return (int)Math.Floor(0.5 + id / (double)PageSize);
        }
    }
}
