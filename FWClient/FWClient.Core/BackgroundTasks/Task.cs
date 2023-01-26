using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FWClient.Core.BackgroundTasks
{
    public class Task
    {
        [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter))]
        public UploadingTaskStatuses Status { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}