using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Net.Http.Headers;
using TvMazeScraper.Api.Application;
using TvMazeScraper.Domain;
using TvMazeScraper.Domain.Anticorruption;
using TvMazeScraper.Infrastructure;
using TvMazeScraper.Infrastructure.Persistence.DB;

namespace TvMazeScraper.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new Info
                        {
                            Title = "Tv Maze Scraper API",
                            Version = "v1",
                            Contact = new Contact {Name = "Ilker Tasatan", Email = "itasatan@gmail.com"},
                            Description = "This is a help document explains how to call TV shows from API resources."
                        });
                });

            services.Configure<TvMazeApiSetting>(Configuration.GetSection("AnticorruptionSettings:TvMazeApiSetting"));
            services.Configure<MongoSettings>(Configuration.GetSection("DbSettings:Mongo"));

            services.AddOptions();

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

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new InfrastructureModule());
            containerBuilder.RegisterModule(new AnticorruptionModule());
            containerBuilder.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TV Maze Scraper API v1");
            });
        }
    }
}
