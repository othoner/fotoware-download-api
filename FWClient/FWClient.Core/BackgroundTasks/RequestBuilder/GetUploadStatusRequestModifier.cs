namespace FWClient.Core.BackgroundTasks.RequestBuilder
{
    /// <summary>
    /// Modify http request to get upload task status.
    /// </summary>
    internal class GetUploadStatusRequestModifier : IRequestModifier
    {
        /// <inheritdoc />
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
}