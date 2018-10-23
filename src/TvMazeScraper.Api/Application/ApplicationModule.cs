using Autofac;
using TvMazeScraper.Api.Application.Services;
using TvMazeScraper.Api.Application.Services.Abstractions;

namespace TvMazeScraper.Api.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TvMazeScraperService>().As<ITvMazeScraperService>().SingleInstance();
        }
    }
}
