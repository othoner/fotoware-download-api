using FWClient.Core.Assets;
using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class ArchiveDetails : CollectionItemBase
    {
        [JsonProperty("assetCount")]
        public int? AssetCount { get; set; }

        [JsonProperty("assets")]
        public CollectionList<Asset> Assets { get; set; }

        [JsonProperty("childCount")]
        public int? ChildCount { get; set; }
    }
}