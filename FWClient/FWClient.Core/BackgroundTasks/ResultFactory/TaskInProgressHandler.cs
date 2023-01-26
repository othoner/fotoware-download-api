using System.Net;

namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal class TaskInProgressHandler : IHttpResponseHandler
    {
        public bool CanHandle(HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.Accepted;
        }

        public Task<BackgroundTaskResult> HandleAsync(HttpResponseMessage response)
        {
            return System.Threading.Tasks.Task.FromResult<BackgroundTaskResult>(new TaskInProgressResult());
        }
    }
}