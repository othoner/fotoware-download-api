using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class PosterImage
    {
        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("square")]
        public bool? Square { get; set; }
    }
}