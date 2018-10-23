using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using TvMazeScraper.Domain.Anticorruption.Abstractions;

namespace TvMazeScraper.Domain.Anticorruption
{
    public class TvMazeRequestBuilder : HttpClientBase, ITvMazeRequestBuilder
    {
        private const string ClientName = "TvMazeApiHttpClient";
        private const int HttpStatusCodeReachedRateLimit = 429;

        private readonly TvMazeApiSetting setting;
        private readonly ILogger<TvMazeRequestBuilder> logger;

        public TvMazeRequestBuilder(IHttpClientFactory httpClientFactory,
            IOptions<TvMazeApiSetting> setting, ILogger<TvMazeRequestBuilder> logger) : base(httpClientFactory.CreateClient(ClientName))
        {
            this.setting = setting.Value;
            this.logger = logger;

            this.CurrentHttpClient.Timeout = TimeSpan.FromSeconds(this.setting.Timeout);
            this.CurrentHttpClient.BaseAddress = new UriBuilder(
                this.setting.Scheme,
                this.setting.Host,
                this.setting.Port).Uri;
        }

        public async Task<HttpResponseMessage> GetAllShows(int pageIndex)
        {
            return await Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == (HttpStatusCode)HttpStatusCodeReachedRateLimit)
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(30), (result, timeSpan, retryCount, context) =>
                {
                    this.logger.LogWarning($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                })
                .ExecuteAsync(() =>
                {
                    var url = string.Format(this.setting.ApiUrl.ShowsUrl, pageIndex);
                    return base.Get(url);
                });
        }

        public async Task<HttpResponseMessage> GetCastByShowId(int showId)
        {
            return await Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == (HttpStatusCode)HttpStatusCodeReachedRateLimit)
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(30), (result, timeSpan, retryCount, context) =>
                {
                    this.logger.LogWarning($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                })
                .ExecuteAsync(() =>
                {
                    var url = string.Format(this.setting.ApiUrl.ShowCastUrl, showId);
                    return base.Get(url);
                });
        }
    }
}
