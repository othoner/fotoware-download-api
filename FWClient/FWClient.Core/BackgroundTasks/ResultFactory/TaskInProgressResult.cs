namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    public class TaskInProgressResult : BackgroundTaskResult
    {
        public TaskInProgressResult() : base(BackgroundTaskStatus.InProgress)
        {
        }
    }
}