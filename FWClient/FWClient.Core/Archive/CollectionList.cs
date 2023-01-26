using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class CollectionList<T>
    where T : CollectionItemBase
    {
        [JsonProperty("searchURL")]
        public string? SearchUrl { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("paging")]
        public PagingInfo? Paging { get; set; }
    }
}