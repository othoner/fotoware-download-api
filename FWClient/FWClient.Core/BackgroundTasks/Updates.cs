using Newtonsoft.Json;

namespace FWClient.Core.BackgroundTasks
{
    /// <summary>
    /// Polling frequency for a background task.
    /// </summary>
    public class Updates
    {
        /// <summary>
        /// Gets or sets recommended time to wait(in milliseconds) until making the next poll request.
        /// </summary>
        [JsonProperty("frequency")]
        public int Frequency { get; set; }

        /// <summary>
        /// Gets or sets URL of the background task for making the next polling request.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets type of a background task.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}