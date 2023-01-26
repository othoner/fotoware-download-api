namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal interface IHttpResponseHandler
    {
        public bool CanHandle(HttpResponseMessage response);

        public Task<BackgroundTaskResult> HandleAsync(HttpResponseMessage response);
    }
}