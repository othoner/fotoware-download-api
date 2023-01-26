using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class CollectionItemBase
    {
        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("searchString")]
        public string SearchString { get; set; }

        [JsonProperty("searchURL")]
        public string SearchURL { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("searchQuery")]
        public string SearchQuery { get; set; }
    }
}