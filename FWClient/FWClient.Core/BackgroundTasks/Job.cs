using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FWClient.Core.BackgroundTasks
{
    /// <summary>
    /// Job info.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Gets or sets attribute of an upload task result.
        /// </summary>
        [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter))]
        public UploadingTaskStatuses Status { get; set; }

        /// <summary>
        /// Gets or sets result of the background task.
        /// </summary>
        [JsonProperty("result")]
        public List<JobResult> Result { get; set; }

        /// <summary>
        /// Gets or sets polling frequency for a background task.
        /// </summary>
        [JsonProperty("updates")]
        public Updates? Updates { get; set; }
    }
}