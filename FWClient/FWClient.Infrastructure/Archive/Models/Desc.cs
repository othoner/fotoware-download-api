using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Desc
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("default")]
        public bool? Default { get; set; }

        [JsonProperty("urlComponents")]
        public List<UrlComponent> UrlComponents { get; set; }
    }
}