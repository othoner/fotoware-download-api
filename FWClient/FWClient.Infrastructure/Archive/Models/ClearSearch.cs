using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class ClearSearch
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}