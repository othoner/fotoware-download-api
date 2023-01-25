using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FWClient.Core.Archive;
using FWClient.Core.Assets;

namespace Download.Crawl
{
    internal class CrawlService : ICrawlService
    {
        private readonly IArchiveManager _archiveManager;

        public CrawlService(IArchiveManager archiveManager)
        {
            _archiveManager = archiveManager ?? throw new ArgumentNullException(nameof(archiveManager));
        }

        public async Task<Asset> FirstAvailableAssetAsync()
        {
            var archives = await _archiveManager.GetAll();

            if (!archives.Data.Any())
            {
                return null;
            }

            ArchiveDetails archDetail = null;
            await foreach (var arch in FetchArchivesAsync(archives))
            {
                // ToDo: Add folder crawl script
                if (arch.AssetCount > 0)
                {
                    archDetail = arch;
                    break;
                }
            }

            return archDetail?.Assets.Data.FirstOrDefault();
        }

        private async IAsyncEnumerable<ArchiveDetails> FetchArchivesAsync(ArchiveCollectionList archives)
        {
            foreach (var fetchTask in archives.Data.Select(t => _archiveManager.GeyByTaxonomyAsync(t)))
            {
                yield return await fetchTask;
            }
        }
    }
}