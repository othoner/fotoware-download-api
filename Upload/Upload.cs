using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using UploadAPI.Model;

namespace UploadAPI
{
    public class Upload : IUpload
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IRequestHandler _requestHandler;

        public Upload(ILogger<Upload> logger, IHttpClientFactory httpFactory, IRequestHandler requestHandler)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _requestHandler = requestHandler;
        }

        public UploadTaskResponse CreateUploadTask(string accessToken, UploadDetails uploadDetails)
        {
            var fileLength = new FileInfo(uploadDetails.FullFilePath).Length;

            var httpClient = _httpFactory.CreateClient("FotoWebApi");
            var apiPath = "api/uploads";
            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(httpClient.BaseAddress + apiPath),
                Content = JsonContent.Create<UploadTaskRequest>(new UploadTaskRequest
                {
                    Destination = uploadDetails.Desitnation,
                    Folder = uploadDetails.Folder,
                    FileName = Path.GetFileName(uploadDetails.FullFilePath), //fileName,
                    FileSize = fileLength,
                    CheckoutId = null,
                    HasXmp = false,
                    IgnoreMetadata = false,
                    //Metadata = new MetaData
                    //{
                    //    Fields = new List<MetaDataField> {
                    //            new MetaDataField { Id = 5, Action = "Add", Value = new List<string> { uploadDetails .Title} },
                    //    },
                    //    Attributes = new List<KeyValuePair<string, string>>(),
                    //},
                    //Comment = uploadDetails.Comment
                }),

            };
            requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

            var result = httpClient.SendAsync(requestData).Result;
            _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");
            var a = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<UploadTaskResponse>(a);
        }

        public void UploadFile(string accessToken, UploadDetails uploadDetails, UploadTaskResponse uploadTaskResponse)
        {
            var httpClient = _httpFactory.CreateClient("FotoWebApi");
            var apiPath = $"api/uploads/{uploadTaskResponse.Id}/chunks";
            var boundary = Guid.NewGuid().ToString();
            using (Stream fileStream = new FileStream(uploadDetails.FullFilePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead = 0;
                long bytesToRead = fileStream.Length;
                int chunkIterator = 0;
                while (bytesToRead > 0)
                {
                    var chunkSize = uploadTaskResponse.ChunkSize;
                    if (bytesToRead < uploadTaskResponse.ChunkSize)
                    {
                        chunkSize = Convert.ToInt32(bytesToRead);
                    }

                    byte[] buffer = new byte[chunkSize];
                    int bytesReadToBuffer = fileStream.Read(buffer, 0, chunkSize);

                    if (bytesReadToBuffer == 0)
                    {
                        break;
                    }

                    // do work on buffer...
                    // uploading chunk ....

                    var formDataStream = new MemoryStream();
                    string header = $"--{boundary}\r\nContent-Disposition: form-data; name=\"chunk\"; filename=\"chunk\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    formDataStream.Write(Encoding.ASCII.GetBytes(header), 0, Encoding.ASCII.GetByteCount(header));
                    formDataStream.Write(buffer, 0, buffer.Length);
                    string footer = "\r\n--" + boundary + "--\r\n";
                    formDataStream.Write(Encoding.ASCII.GetBytes(footer), 0, Encoding.ASCII.GetByteCount(footer));

                    formDataStream.Position = 0;
                    var requestData = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{httpClient.BaseAddress}{apiPath}/{chunkIterator}"),
                        Content = new StreamContent(formDataStream),
                    };

                    requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                    requestData.Content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                    requestData.Content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));
                    var result = httpClient.SendAsync(requestData).Result;
                    _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");

                    bytesRead += bytesReadToBuffer;
                    bytesToRead -= bytesReadToBuffer;

                }
                fileStream.Dispose();
            }
        }
        public StatusResult GetUploadStatus(string accessToken, string uploadId)
        {
            var httpClient = _httpFactory.CreateClient("FotoWebApi");
            var apiPath = $"api/uploads/{uploadId}/status";
            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(httpClient.BaseAddress + apiPath),
            };
            requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

            var result = httpClient.SendAsync(requestData).Result;
            _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(result)}");
            return JsonConvert.DeserializeObject<StatusResult>(result.Content.ReadAsStringAsync().Result);
        }
    }
}
