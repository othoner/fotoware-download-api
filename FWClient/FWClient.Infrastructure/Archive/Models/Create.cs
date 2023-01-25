using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Create
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}