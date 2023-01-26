namespace FWClient.Core.BackgroundTasks.RequestBuilder
{
    /// <summary>
    /// Modify http request to get rendition task status.
    /// </summary>
    internal class RenditionRequestModifier : IRequestModifier
    {
        /// <inheritdoc />
        public void Modify(RequestedTaskInfo taskInfo, HttpRequestMessage requestMessage)
        {
            if (taskInfo.Type != TaskType.RenditionResponse)
            {
                return;
            }

            requestMessage.RequestUri = new Uri(Path.Combine(BackgroundTaskManager.ApiPath, taskInfo.TaskId), UriKind.Relative);
            requestMessage.Headers.Add("Accept", "application/vnd.fotoware.rendition-response+json");
        }
    }
}