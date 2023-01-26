using FWClient.Core.BackgroundTasks.ResultFactory;
using FWClient.Core.Common;
using Microsoft.Extensions.Logging;

namespace FWClient.Core.BackgroundTasks
{
    internal class BackgroundTaskManager : GenericFwManager<BackgroundTaskManager>, IBackgroundTaskManager
    {
        public const string ApiPath = "/fotoweb/me/background-tasks/";
        
        private readonly IBackgroundTaskRequestBuilder _requestBuilder;
        private readonly IBackgroundTaskResultFactory _backgroundTaskResultFactory;

        public BackgroundTaskManager(
            ILogger<BackgroundTaskManager> logger,
            IHttpClientFactory clientFactory,
            IBackgroundTaskRequestBuilder requestBuilder,
            IBackgroundTaskResultFactory backgroundTaskResultFactory) :
            base(logger, clientFactory)
        {
            _requestBuilder = requestBuilder ?? throw new ArgumentNullException(nameof(requestBuilder));
            _backgroundTaskResultFactory = backgroundTaskResultFactory ?? throw new ArgumentNullException(nameof(backgroundTaskResultFactory));
        }

        public async Task<BackgroundTaskResult> GetTaskStatusAsync(RequestedTaskInfo taskInfo)
        {
            using var requestData = _requestBuilder.GenerateRequest(taskInfo);

            var response = await HttpClient.SendAsync(requestData);

            response.EnsureSuccessStatusCode();

            var result = await _backgroundTaskResultFactory.GetTaskResultAsync(response);

            return result;
        }
    }
}