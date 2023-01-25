using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Props
    {
        [JsonProperty("annotations")]
        public Annotations Annotations { get; set; }

        [JsonProperty("shares")]
        public Shares Shares { get; set; }

        [JsonProperty("comments")]
        public Comments Comments { get; set; }
    }
}