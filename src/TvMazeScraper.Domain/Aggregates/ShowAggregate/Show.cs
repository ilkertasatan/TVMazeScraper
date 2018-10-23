using Newtonsoft.Json;
using System.Collections.Generic;
using TvMazeScraper.Domain.Abstractions;
using TvMazeScraper.Domain.Aggregates.ValueObjects;
using TvMazeScraper.Domain.SeedWork;

namespace TvMazeScraper.Domain.Aggregates.ShowAggregate
{
    public class Show : Entity, IAggregateRoot
    {
        public Show()
        {
            this.Actors = new List<Actor>();
        }

        public int ExternalId { get; set; }
        public string Name { get; set; }
        public IList<Actor> Actors { get; set; }
    }
}
