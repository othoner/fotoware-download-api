namespace FWClient.Core.Renditions
{
    public interface IRenditionManager
    {
        Task<RenditionResult> SubmitRenditionAsync(string href);
    }
}