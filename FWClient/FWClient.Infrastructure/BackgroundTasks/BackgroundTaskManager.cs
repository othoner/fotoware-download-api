using FWClient.Core.Renditions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FWClient.Core.BackgroundTasks
{
    internal class BackgroundTaskManager : IBackgroundTaskManager
    {
        public const string ApiPath = "me/background-tasks/";
        
        private readonly ILogger<RenditionManager> _logger;

        private readonly HttpClient _httpClient;

        public BackgroundTaskManager(ILogger<RenditionManager> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = clientFactory.CreateClient(DependencyInjection.FwHttpClient);
        }

        public async Task<UploadStatus> GetTaskStatusAsync(string taskId)
        {
            using var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Path.Combine(ApiPath, taskId), UriKind.Relative)
            };

            // ToDo: Consider setting different headers.
            ////requestData.Headers.TryAddWithoutValidation("Accept", "application/vnd.fotoware.rendition-response+json");

            var response = await _httpClient.SendAsync(requestData);

            var result = JsonConvert.DeserializeObject<UploadStatus>(await response.Content.ReadAsStringAsync());

            return result;
        }
    }
}