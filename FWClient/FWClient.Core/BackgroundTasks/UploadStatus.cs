namespace FWClient.Core.BackgroundTasks
{
    /// <summary>
    /// Represents the status of the background task.
    /// </summary>
    public class UploadStatus
    {
        public Job Job { get; set; }

        public Task Task { get; set; }
    }
}