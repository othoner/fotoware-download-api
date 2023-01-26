using Newtonsoft.Json;

namespace FWClient.Core.Renditions
{
    public class RenditionRequest
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}