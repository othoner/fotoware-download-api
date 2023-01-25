using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using FWClient.Core.BackgroundTasks;
using FWClient.Core.Renditions;

namespace DownloadAPI
{
    public class DownloadService
    {
        private readonly ILogger _logger;
        private readonly IBackgroundTaskManager _backgroundTaskManager;
        private readonly IRenditionManager _renditionManager;

        public DownloadService(ILogger<DownloadService> logger, IRenditionManager renditionManager, IBackgroundTaskManager backgroundTaskManager)
        {
            this._renditionManager = renditionManager ?? throw new ArgumentNullException(nameof(renditionManager));
            this._backgroundTaskManager = backgroundTaskManager ?? throw new ArgumentNullException(nameof(backgroundTaskManager));
        }

        public async Task<byte[]> DownloadAsset(string accessToken, string backgroundTaskUrl)
        {
            HttpResponseMessage lastResponseMessage = new HttpResponseMessage();
            do
            {
                var requestData = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    //RequestUri = new Uri(_httpClient.BaseAddress + backgroundTaskUrl)
                };
                requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                requestData.Headers.TryAddWithoutValidation("Accept", "application/json");

                //var responseMessage = await _httpClient.SendAsync(requestData);
                //lastResponseMessage = responseMessage;
            }
            while (lastResponseMessage.StatusCode == System.Net.HttpStatusCode.Accepted);

            if(lastResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var fileContent = await lastResponseMessage.Content.ReadAsByteArrayAsync();
                return fileContent;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<string> SaveFile(byte[] fileData, string fileName)
        { 
            //if(response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
    
                using (var stream = new FileStream("c:\\Temp\\Download\\" + fileName, FileMode.CreateNew, FileAccess.Write))
                {
                    await stream.WriteAsync(fileData, 0, fileData.Length);
                }
            //}

            //TODO: Handle errors or the background task not complete.

            return "c:\\Temp\\Download\\" + fileName;
        }

        public void GetAsset(string accessToken)
        {
            var assetUrl = "https://oyvindthoner.fotoware.cloud/fotoweb/archives/5002-Photos/Folder%2019/nweweq%20(5).jpg.info";
            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(assetUrl)
            };
            requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
            requestData.Headers.TryAddWithoutValidation("Accept", "application/json");
            //requestData.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.fotoware.rendition-request+json");

            //var result = _httpClient.SendAsync(requestData).Result;
            ////_logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");
        }
    }
}
