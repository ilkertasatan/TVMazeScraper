using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Console;
using TvMazeScraper.Application.Services;
using TvMazeScraper.Domain;
using TvMazeScraper.Domain.Anticorruption;
using TvMazeScraper.Infrastructure;
using TvMazeScraper.Infrastructure.Persistence.DB;

namespace TvMazeScraper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("ASPNETCORE_");
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    ConfigureApp(hostingContext, config);
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    Console.WriteLine(hostingContext.HostingEnvironment.EnvironmentName);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureAutoFacContainer)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging);

            
            await builder.RunConsoleAsync();
        }

        private static void ConfigureAutoFacContainer(HostBuilderContext hostingContext, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new InfrastructureModule());
            containerBuilder.RegisterModule(new AnticorruptionModule());
            containerBuilder.RegisterModule(new ApplicationModule());
        }

        private static void ConfigureLogging(HostBuilderContext hostingContext, ILoggingBuilder logging)
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
        }

        private static void ConfigureApp(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();
        }

        private static void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
        {
            services.Configure<TvMazeApiSetting>(hostingContext.Configuration.GetSection("AnticorruptionSettings:TvMazeApiSetting"));
            services.Configure<MongoSettings>(hostingContext.Configuration.GetSection("DbSettings:Mongo"));

            services.AddOptions();

            services.AddScoped<IHostedService, TvMazeScraperControlService>();

            services.AddHttpClient
            (
                "TvMazeApiHttpClient",
                client =>
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            );
        }
    }
}
