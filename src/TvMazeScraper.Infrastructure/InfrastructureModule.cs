using Autofac;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TvMazeScraper.Infrastructure.Persistence.DB;
using TvMazeScraper.Infrastructure.Persistence.DB.Abstractions;

namespace TvMazeScraper.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
                {
                    var dbSettings = context.Resolve<IOptions<MongoSettings>>().Value;

                    var mongoClient = new MongoClient(dbSettings.ConnectionString);

                    return mongoClient.GetDatabase(dbSettings.DatabaseName);
                })
                .As<IMongoDatabase>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<TvMazeScraperDatabase>().As<ITvMazeScraperDatabase>().SingleInstance();
        }
    }
}
