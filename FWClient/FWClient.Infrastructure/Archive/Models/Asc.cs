using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Asc
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("urlComponents")]
        public List<UrlComponent> UrlComponents { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}