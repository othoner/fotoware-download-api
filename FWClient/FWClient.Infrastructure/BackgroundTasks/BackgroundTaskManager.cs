using FWClient.Core.BackgroundTasks.ResultFactory;
using FWClient.Core.Common;
using Microsoft.Extensions.Logging;

namespace FWClient.Core.BackgroundTasks
{
    internal class BackgroundTaskManager : GenericFwManager<BackgroundTaskManager>, IBackgroundTaskManager
    {
        private readonly IBackgroundTaskResultFactory _backgroundTaskResultFactory;
        public const string ApiPath = "fotoweb/me/background-tasks/";

        public BackgroundTaskManager(ILogger<BackgroundTaskManager> logger, IHttpClientFactory clientFactory, IBackgroundTaskResultFactory backgroundTaskResultFactory) : base(logger, clientFactory)
        {
            _backgroundTaskResultFactory = backgroundTaskResultFactory ?? throw new ArgumentNullException(nameof(backgroundTaskResultFactory));
        }

        public async Task<BackgroundTaskResult> GetTaskStatusAsync(string taskId)
        {
            using var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Path.Combine(ApiPath, taskId), UriKind.Relative)
            };

            // ToDo: Consider setting different headers.
            requestData.Headers.TryAddWithoutValidation("Accept", "application/vnd.fotoware.rendition-response+json");

            var response = await HttpClient.SendAsync(requestData);

            var result = await _backgroundTaskResultFactory.GetTaskResultAsync(response);

            return result;
        }
    }
}