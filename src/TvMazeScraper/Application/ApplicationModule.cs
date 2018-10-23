using Autofac;
using TvMazeScraper.Application.Services;
using TvMazeScraper.Application.Services.Abstractions;

namespace TvMazeScraper
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TvMazeScraperService>().As<ITvMazeScraperService>().SingleInstance();
        }
    }
}
