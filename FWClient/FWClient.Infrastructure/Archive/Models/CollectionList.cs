using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class CollectionList
    {
        [JsonProperty("searchURL")]
        public string? SearchURL { get; set; }

        [JsonProperty("data")]
        public List<TaxonomyItemInfo> Data { get; set; }

        [JsonProperty("paging")]
        public PagingInfo? Paging { get; set; }
    }
}