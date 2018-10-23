namespace TvMazeScraper.Domain.Anticorruption
{
    public class TvMazeApiSetting
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public TvMazeApiUrlSetting ApiUrl { get; set; }

    }

    public class TvMazeApiUrlSetting
    {
        public string ShowsUrl { get; set; }
        public string ShowCastUrl { get; set; }
    }
}
