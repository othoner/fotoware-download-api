namespace FWClient.Core.BackgroundTasks.RequestBuilder;

internal class GetUploadStatusRequestModifier : IRequestModifier
{
    public void Modify(RequestedTaskInfo taskInfo, HttpRequestMessage requestMessage)
    {
        if (taskInfo.Type != TaskType.UploadStatus)
        {
            return;
        }

        requestMessage.RequestUri = new Uri(Path.Combine(BackgroundTaskManager.ApiPath, taskInfo.TaskId), UriKind.Relative);
        requestMessage.Headers.Add("Accept", "application/vnd.fotoware.upload-status+json");
    }
}