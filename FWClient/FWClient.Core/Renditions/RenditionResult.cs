using FWClient.Core.BackgroundTasks;
using Newtonsoft.Json;

namespace FWClient.Core.Renditions
{
    public class RenditionResult
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        public string TaskId
        {
            get => Href.Remove(0, BackgroundTaskManager.ApiPath.Length);
        }
    }
}