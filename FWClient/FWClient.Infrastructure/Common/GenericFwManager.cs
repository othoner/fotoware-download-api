using Microsoft.Extensions.Logging;

namespace FWClient.Core.Common
{
    internal abstract class GenericFwManager<T>
    {
        protected ILogger<T> Logger { get; }
        protected HttpClient HttpClient { get; }

        public GenericFwManager(ILogger<T> logger, IHttpClientFactory clientFactory)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            HttpClient = clientFactory.CreateClient(DependencyInjection.FwHttpClient);
        }
    }
}