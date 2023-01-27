using Newtonsoft.Json;

namespace FWClient.Core.Assets
{
    public class ImageAttributes
    {
        [JsonProperty("pixelwidth")]
        public int PixelWidth { get; set; }

        [JsonProperty("pixelheight")]
        public int PixelHeight { get; set; }

        [JsonProperty("resolution")]
        public double Resolution { get; set; }

        [JsonProperty("flipmirror")]
        public int FlipMirror { get; set; }

        [JsonProperty("rotation")]
        public double Rotation { get; set; }

        [JsonProperty("colorspace")]
        public string ColorSpace { get; set; }
    }
}