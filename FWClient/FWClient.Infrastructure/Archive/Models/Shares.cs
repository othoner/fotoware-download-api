using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Shares
    {
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}