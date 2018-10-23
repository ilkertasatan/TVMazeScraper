using Autofac;
using TvMazeScraper.Domain.Anticorruption.Abstractions;
using TvMazeScraper.Domain.Anticorruption.ShowCast;
using TvMazeScraper.Domain.Anticorruption.ShowCast.Abstractions;
using TvMazeScraper.Domain.Anticorruption.Shows;
using TvMazeScraper.Domain.Anticorruption.Shows.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption
{
    public class AnticorruptionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShowsAnticorruption>().As<IShowsAnticorruption>().SingleInstance();
            builder.RegisterType<ShowsTranslator>().As<IShowsTranslator>().SingleInstance();

            builder.RegisterType<ShowCastAnticorruption>().As<IShowCastAnticorruption>().SingleInstance();
            builder.RegisterType<ShowCastTranslator>().As<IShowCastTranslator>().SingleInstance();

            builder.RegisterType<TvMazeRequestBuilder>().As<ITvMazeRequestBuilder>().SingleInstance();
        }
    }
}
