using System.Net;

namespace FWClient.Core.BackgroundTasks.ResultFactory
{
    internal class DownloadAssetHandler : IHttpResponseHandler
    {
        public bool CanHandle(HttpResponseMessage response)
        {
            //// ToDo: remove commented code
            //// var canHandleAsync = response.Headers.TryGetValues("Content-Type", out var header) && header.Any(h => h.Equals("application/octet-stream", StringComparison.OrdinalIgnoreCase));

            return response.StatusCode == HttpStatusCode.OK
                   && response.Content.Headers.ContentType.MediaType.Equals("application/octet-stream", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<BackgroundTaskResult> HandleAsync(HttpResponseMessage response)
        {
            // JsonConvert.DeserializeObject<UploadStatus>(await response.Content.ReadAsStringAsync());
            return new AssetReadyToBeDownloadedResult(await response.Content.ReadAsByteArrayAsync());
        }
    }
}