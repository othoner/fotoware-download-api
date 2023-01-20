using DownloadAPI.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using DownloadAPI.Model.Inriver;

namespace DownloadAPI
{
    public class Download : IDownload
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientInriver;
        private readonly IRequestHandler _requestHandler;

        public Download(ILogger<Download> logger, IHttpClientFactory httpFactory, IRequestHandler requestHandler)
        {
            _logger = logger;
            _requestHandler = requestHandler;
            _httpClient = httpFactory.CreateClient("FotoWebApi");
            _httpClientInriver = httpFactory.CreateClient("InriverApi");
        }

        public DownloadTaskResponse CreateDownloadTask(string accessToken, string renditionUrl)
        {
            var apiPath = "/fotoweb/services/renditions";
            var downloadRequest = new DownloadTaskRequest
            {
                href = renditionUrl
            };
            var stringRequest = JsonConvert.SerializeObject(downloadRequest);
            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress + apiPath),
                Content = new StringContent(stringRequest, Encoding.UTF8)
            };
            requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
            requestData.Headers.TryAddWithoutValidation("Accept", "application/vnd.fotoware.rendition-response+json");
            requestData.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.fotoware.rendition-request+json");

            var result = _httpClient.SendAsync(requestData).Result;
            _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");

            return JsonConvert.DeserializeObject<DownloadTaskResponse>(result.Content.ReadAsStringAsync().Result);
        }

        public async Task<byte[]> DownloadAsset(string accessToken, string backgroundTaskUrl)
        {
            HttpResponseMessage lastResponseMessage;
            do
            {
                var requestData = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_httpClient.BaseAddress + backgroundTaskUrl)
                };
                requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                requestData.Headers.TryAddWithoutValidation("Accept", "application/json");

                var responseMessage = await _httpClient.SendAsync(requestData);
                lastResponseMessage = responseMessage;
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

        public async void UploadToInriver(HttpResponseMessage response, string fileName)
        {
            var apiPath = "media:uploadbase64";
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var base64FileData = Convert.ToBase64String(bytes);

            var inriverData = new InriverUpload
            {
                FileName = fileName,
                Data = base64FileData
            };

            var stringRequest = JsonConvert.SerializeObject(inriverData);

            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClientInriver.BaseAddress + apiPath),
                Content = new StringContent(stringRequest, Encoding.UTF8)
            };
            requestData.Headers.TryAddWithoutValidation("X-inRiver-APIKey", "f21bd857b477380732580364c703e9f8");
            requestData.Headers.TryAddWithoutValidation("Accept", "application/json");
            requestData.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = _httpClientInriver.SendAsync(requestData).Result;
            var a = result.StatusCode;
            //_logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");
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

            var result = _httpClient.SendAsync(requestData).Result;
            _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");
        }
    }
}
