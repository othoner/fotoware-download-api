using FWClient.Core.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace FWClient.Core.Renditions
{
    internal class RenditionManager : GenericFwManager<RenditionManager>, IRenditionManager
    {
        private const string ApiPath = "/fotoweb/services/renditions";

        public RenditionManager(ILogger<RenditionManager> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<RenditionResult> SubmitRenditionAsync(string href)
        {
            var request = new RenditionRequest { Href = href };
            var content = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/vnd.fotoware.rendition-request+json");

            using var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiPath, UriKind.Relative),
                Content = httpContent
            };

            requestData.Headers.TryAddWithoutValidation("Accept", "application/vnd.fotoware.rendition-response+json");

            try
            {
                var response = await HttpClient.SendAsync(requestData);

                var result = JsonConvert.DeserializeObject<RenditionResult>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}