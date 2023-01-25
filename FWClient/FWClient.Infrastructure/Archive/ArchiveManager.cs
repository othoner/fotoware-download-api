using FWClient.Core.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    internal class ArchiveManager : GenericFwManager<ArchiveManager>, IArchiveManager
    {
        private const string ApiPath = "fotoweb/archives";

        public ArchiveManager(ILogger<ArchiveManager> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<CollectionList> GetAll(string? query = null)
        {
            var uri = string.IsNullOrWhiteSpace(query) ? ApiPath : $"{ApiPath}?q={Uri.EscapeDataString(query)}";
            var response = await this.HttpClient.GetAsync(new Uri(uri, UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request exception");
            }

            try
            {
                var result = JsonConvert.DeserializeObject<CollectionList>(await response.Content.ReadAsStringAsync());

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