namespace FWClient.Core.BackgroundTasks
{
    public class UploadTaskStatusResult : BackgroundTaskResult
    {
        public UploadStatus UploadStatus { get; }

        public UploadTaskStatusResult(UploadStatus uploadStatus) : base(BackgroundTaskStatus.UploadStatus)
        {
            this.UploadStatus = uploadStatus ?? throw new ArgumentNullException(nameof(uploadStatus));
        }
    }
}