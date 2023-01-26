using Newtonsoft.Json;

namespace FWClient.Core.Assets
{
    public class AssetRendition
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("original")]
        public bool Original { get; set; }

        [JsonProperty("sizeFixed")]
        public bool SizeFixed { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }
    }
}