using Autofac;
using System.Reflection;
using TvMazeScraper.Domain.Abstractions;
using TvMazeScraper.Domain.Aggregates.ShowAggregate;
using TvMazeScraper.Domain.Aggregates.ShowAggregate.Abstractions;
using Module = Autofac.Module;

namespace TvMazeScraper.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShowRepository>().As<IShowRepository>().SingleInstance();
            builder.RegisterType<ShowDomainService>().As<IShowDomainService>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t)).InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
