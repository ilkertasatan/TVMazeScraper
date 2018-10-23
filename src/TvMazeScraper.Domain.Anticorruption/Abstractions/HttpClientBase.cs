using System.Net.Http;
using System.Threading.Tasks;

namespace TvMazeScraper.Domain.Anticorruption.Abstractions
{
    public abstract class HttpClientBase
    {
        protected HttpClientBase(HttpClient httpClient)
        {
            CurrentHttpClient = httpClient;
        }

        protected HttpClient CurrentHttpClient { get; }

        protected virtual async Task<HttpResponseMessage> Get(string uri)
        {
            return await this.CurrentHttpClient.GetAsync(uri);
        }
    }
}
