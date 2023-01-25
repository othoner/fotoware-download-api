using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class UrlComponent
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}