namespace FWClient.Core.Archive
{
    public interface IArchiveManager
    {
        Task<ArchiveCollectionList> GetAll(string? query = null);
        Task<ArchiveDetails> GeyByIdAsync(string id);
        Task<ArchiveDetails> GeyByTaxonomyAsync(TaxonomyItemInfo taxonomyItemInfo);
    }
}