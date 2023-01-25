namespace FWClient.Core.BackgroundTasks
{
    public interface IBackgroundTaskManager
    {
        Task<BackgroundTaskResult> GetTaskStatusAsync(string taskId);
    }
}