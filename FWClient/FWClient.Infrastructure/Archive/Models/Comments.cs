using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Comments
    {
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}