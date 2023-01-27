namespace FWClient.Core.Renditions
{
    /// <summary>
    /// Manage operation on renditions.
    /// </summary>
    public interface IRenditionManager
    {
        /// <summary>
        /// Sends rendition requests for downloading renditions via the API.
        /// </summary>
        /// <param name="href">Rendition URL.</param>
        /// <returns>Download task id.</returns>
        Task<RenditionResult> SubmitRenditionAsync(string href);
    }
}