using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace FWClient.Core.BackgroundTasks
{
    public class Job
    {
        [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter))]
        public UploadingTaskStatuses Status { get; set; }

        [JsonProperty("result")]
        public List<JobResult> Result { get; set; }

        [JsonProperty("updates")]
        public Updates? Updates { get; set; }
    }
}