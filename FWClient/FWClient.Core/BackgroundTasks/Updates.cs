using Newtonsoft.Json;

namespace FWClient.Core.BackgroundTasks;

public class Updates
{
    [JsonProperty("frequency")]
    public int Frequency { get; set; }

    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}