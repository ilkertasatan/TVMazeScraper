using Newtonsoft.Json;
using System.Collections.Generic;

namespace TvMazeScraper.Api.Application.DTO
{
    public class ShowDTO
    {
        public ShowDTO()
        {
            this.Cast = new List<ShowCastDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "cast")]
        public IList<ShowCastDTO> Cast { get; set; }
    }
}