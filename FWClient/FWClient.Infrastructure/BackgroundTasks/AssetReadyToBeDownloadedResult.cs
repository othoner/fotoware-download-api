namespace FWClient.Core.BackgroundTasks
{
    public class AssetReadyToBeDownloadedResult : BackgroundTaskResult
    {
        public byte[] Asset { get; }

        public AssetReadyToBeDownloadedResult(byte[] asset) : base(BackgroundTaskStatus.ReadyToDownload)
        {
            this.Asset = asset ?? throw new ArgumentNullException(nameof(asset));
        }
    }
}