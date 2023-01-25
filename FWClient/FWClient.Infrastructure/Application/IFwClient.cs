using FWClient.Core.Albums;

namespace FWClient.Core.Application
{
    public interface IFwClient
    {
        Task<List<Album>> GetMyAlbums();
    }
}