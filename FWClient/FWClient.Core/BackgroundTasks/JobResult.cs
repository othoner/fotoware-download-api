using FWClient.Core.Assets;

namespace FWClient.Core.BackgroundTasks
{
    /// <summary>
    /// Result of the background task.
    /// </summary>
    public class JobResult
    {
        /// <summary>
        /// Gets or sets the final API URL of the uploaded asset. Can be used for making further API requests to it (e.g., assign additional metadata).
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the upload is complete.
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// Gets or sets the original filename of an uploaded file, as given in the upload request.
        /// </summary>
        public string OriginalFilename { get; set; }

        /// <summary>
        /// Gets or sets symbolic name of the type of error that has occurred uploading this asset.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets human-readable description of error that has occurred uploading this asset.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets full API representation of the asset.
        /// </summary>
        public Asset Asset { get; set; }
    }
}