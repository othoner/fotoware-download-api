namespace FWClient.Core.BackgroundTasks
{
    public abstract class BackgroundTaskResult
    {
        public BackgroundTaskStatus Status { get; }

        protected BackgroundTaskResult(BackgroundTaskStatus satatus)
        {
            this.Status = satatus;
        }
    }
}