namespace FWClient.Core.BackgroundTasks
{
    public class UploadTaskStatusResult : BackgroundTaskResult
    {
        public UploadStatus UploadStatus { get; }

        public UploadTaskStatusResult(UploadStatus uploadStatus) : base(BackgroundTaskStatus.UploadStatus)
        {
            UploadStatus = uploadStatus ?? throw new ArgumentNullException(nameof(uploadStatus));
        }
    }
}