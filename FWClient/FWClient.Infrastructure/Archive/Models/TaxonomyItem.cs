using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class TaxonomyItem
    {
        [JsonProperty("paging")]
        public PagingInfo Paging { get; set; }

        [JsonProperty("data")]
        public List<TaxonomyItemInfo> Data { get; set; }
    }
}