using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class TaxonomyItemInfo : CollectionItemBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}