namespace FWClient.Core.BackgroundTasks;

public enum UploadingTaskStatuses
{
    Pending,
    AwaitingData,
    InProgress,
    Done,
    Failed
}