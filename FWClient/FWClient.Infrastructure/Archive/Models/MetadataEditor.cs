using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class MetadataEditor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}