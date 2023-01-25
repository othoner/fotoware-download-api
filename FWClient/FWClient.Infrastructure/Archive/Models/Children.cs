using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Children
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}