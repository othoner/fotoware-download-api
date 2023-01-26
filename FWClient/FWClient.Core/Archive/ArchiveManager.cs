using FWClient.Core.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    internal class ArchiveManager : GenericFwManager<ArchiveManager>, IArchiveManager
    {
        private const string ApiPath = "/fotoweb/archives";

        public ArchiveManager(ILogger<ArchiveManager> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<ArchiveCollectionList> GetAll(string? query = null)
        {
            var uri = string.IsNullOrWhiteSpace(query) ? ApiPath : $"{ApiPath}?q={Uri.EscapeDataString(query)}";
            var response = await HttpClient.GetAsync(new Uri(uri, UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request exception");
            }

            try
            {
                var result = JsonConvert.DeserializeObject<ArchiveCollectionList>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ArchiveDetails> GeyByIdAsync(string id)
        {
            var response = await HttpClient.GetAsync(new Uri(Path.Combine(ApiPath, Uri.EscapeDataString(id)), UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request exception");
            }

            try
            {
                var result = JsonConvert.DeserializeObject<ArchiveDetails>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ArchiveDetails> GeyByTaxonomyAsync(TaxonomyItemInfo taxonomyItemInfo)
        {
            var response = await HttpClient.GetAsync(new Uri(taxonomyItemInfo.Href, UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request exception");
            }

            try
            {
                var result = JsonConvert.DeserializeObject<ArchiveDetails>(await response.Content.ReadAsStringAsync());

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