namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal interface IBackgroundTaskResultFactory
    {
        Task<BackgroundTaskResult> GetTaskResultAsync(HttpResponseMessage response);
    }
}