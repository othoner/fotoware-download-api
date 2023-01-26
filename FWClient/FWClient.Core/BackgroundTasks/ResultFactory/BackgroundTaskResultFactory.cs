namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal class BackgroundTaskResultFactory : IBackgroundTaskResultFactory
    {
        private readonly IEnumerable<IHttpResponseHandler> _handlers = new IHttpResponseHandler[]
        {
            new TaskInProgressHandler(),
            new DownloadAssetHandler(),
            new UploadStatusHandler()
        };

        public async Task<BackgroundTaskResult> GetTaskResultAsync(HttpResponseMessage response)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(response));

            if (handler == null)
            {
                throw new HttpResponseHandlerNotFoundException();
            }

            return await handler.HandleAsync(response);
        }
    }
}