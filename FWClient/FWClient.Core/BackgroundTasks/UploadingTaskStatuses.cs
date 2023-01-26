namespace FWClient.Core.BackgroundTasks
{
    /// <summary>
    /// The status of the upload task.
    /// </summary>
    public enum UploadingTaskStatuses
    {
        /// <summary>
        /// Some chunks or the XMP file have not been received yet.
        /// </summary>
        AwaitingData,

        /// <summary>
        /// The upload is finalized and has been queued for processing.
        /// </summary>
        Pending,

        /// <summary>
        /// The upload is finalized and is being processed.
        /// </summary>
        InProgress,

        /// <summary>
        /// The upload has completed successfully and result contains more information.
        /// </summary>
        Done,

        /// <summary>
        /// The upload has failed and error contains more information.
        /// </summary>
        Failed
    }
}