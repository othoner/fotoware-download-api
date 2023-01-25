using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class PropertyValidation
    {
        [JsonProperty("min")]
        public int? Min { get; set; }

        [JsonProperty("max")]
        public int? Max { get; set; }

        [JsonProperty("regex")]
        public string Regex { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}