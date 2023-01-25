using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Annotations
    {
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}