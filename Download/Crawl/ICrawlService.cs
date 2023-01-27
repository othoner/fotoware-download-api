using System.Threading.Tasks;
using FWClient.Core.Assets;

namespace Download.Crawl
{
    internal interface ICrawlService
    {
        Task<Asset> FirstAvailableAssetAsync();
    }
}