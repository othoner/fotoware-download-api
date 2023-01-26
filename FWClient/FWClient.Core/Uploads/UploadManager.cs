using FWClient.Core.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FWClient.Core.Uploads
{
    internal class UploadManager : GenericFwManager<UploadManager>, IUploadManager
    {
        private const string ApiPath = "/fotoweb/api/uploads";
        private const string ChunkEndpointPath = "/fotoweb/api/uploads/{0}/chunks";

        public UploadManager(ILogger<UploadManager> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<UploadTaskResponse> CreateUploadTaskAsync(UploadTaskRequest uploadTaskRequest)
        {
            var content = JsonConvert.SerializeObject(uploadTaskRequest);

            try
            {
                var response = await HttpClient.PostAsync(new Uri(ApiPath, UriKind.Relative), new StringContent(content, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<UploadTaskResponse>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UploadFileAsync(string fullFilePath, int chunkSize, string uploadId)
        {
            var boundary = Guid.NewGuid().ToString();
            using (Stream fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead = 0;
                long bytesToRead = fileStream.Length;
                int chunkIterator = 0;
                while (bytesToRead > 0)
                {
                    if (bytesToRead < chunkSize)
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

                    await SendChunkAsync(boundary, buffer, chunkIterator, uploadId);

                    bytesRead += bytesReadToBuffer;
                    bytesToRead -= bytesReadToBuffer;
                }
            }
        }

        private async Task SendChunkAsync(string boundary, byte[] buffer, int chunkIterator, string uploadId)
        {
            // uploading chunk ....

            using var formDataStream = new MemoryStream();
            string header = $"--{boundary}\r\nContent-Disposition: form-data; name=\"chunk\"; filename=\"chunk\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            formDataStream.Write(Encoding.ASCII.GetBytes(header), 0, Encoding.ASCII.GetByteCount(header));
            formDataStream.Write(buffer, 0, buffer.Length);
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.ASCII.GetBytes(footer), 0, Encoding.ASCII.GetByteCount(footer));

            formDataStream.Position = 0;
            using var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Path.Combine(string.Format(ChunkEndpointPath, uploadId), chunkIterator.ToString()), UriKind.Relative),
                Content = new StreamContent(formDataStream)
            };

            requestData.Content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            requestData.Content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));

            var result = await HttpClient.SendAsync(requestData);
            result.EnsureSuccessStatusCode();
        }
    }
}