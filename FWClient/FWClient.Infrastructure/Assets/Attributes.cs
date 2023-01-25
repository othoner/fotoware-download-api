using Newtonsoft.Json;

namespace FWClient.Core.Assets
{
    public class Attributes
    {
        [JsonProperty("imageattributes")]
        public ImageAttributes ImageAttributes { get; set; }
    }
}