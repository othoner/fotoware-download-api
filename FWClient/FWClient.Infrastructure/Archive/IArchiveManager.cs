namespace FWClient.Core.Archive
{
    public interface IArchiveManager
    {
        Task<CollectionList> GetAll(string? query = null);
    }
}