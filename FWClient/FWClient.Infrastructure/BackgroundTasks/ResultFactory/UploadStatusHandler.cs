using System.Net;
using Newtonsoft.Json;

namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal class UploadStatusHandler : IHttpResponseHandler
    {
        public bool CanHandle(HttpResponseMessage response)
        {
            return response.StatusCode != HttpStatusCode.Accepted;
        }

        public async Task<BackgroundTaskResult> HandleAsync(HttpResponseMessage response)
        {
            var uploadStatus = JsonConvert.DeserializeObject<UploadStatus>(await response.Content.ReadAsStringAsync());
            return new UploadTaskStatusResult(uploadStatus);
        }
    }
}