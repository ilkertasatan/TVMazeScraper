using TvMazeScraper.Domain.SeedWork;

namespace TvMazeScraper.Domain.Aggregates.ValueObjects
{
    public class Actor : ValueObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDay { get; set; }
    }
}
