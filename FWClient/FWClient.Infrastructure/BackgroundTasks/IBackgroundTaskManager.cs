namespace FWClient.Core.BackgroundTasks
{
    public interface IBackgroundTaskManager
    {
        Task<UploadStatus> GetTaskStatusAsync(string taskId);
    }
}