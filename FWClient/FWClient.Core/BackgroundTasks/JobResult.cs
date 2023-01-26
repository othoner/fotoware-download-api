using FWClient.Core.Assets;

namespace FWClient.Core.BackgroundTasks;

public class JobResult
{
    public string href { get; set; }
    public bool done { get; set; }
    public string originalFilename { get; set; }
    public string errorCode { get; set; }
    public string errorMessage { get; set; }
    public Asset asset { get; set; }
}