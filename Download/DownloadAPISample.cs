using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Download.Crawl;
using Download.FileManager;
using FWClient.Core.BackgroundTasks;
using FWClient.Core.Renditions;
using Microsoft.Extensions.Logging;

namespace Download
{
    /// <summary>
    /// Download asset sample.
    /// </summary>
    internal class DownloadApiSample
    {
        private readonly ILogger _logger;
        private readonly IRenditionManager _renditionManager;
        private readonly IBackgroundTaskManager _backgroundTaskManager;
        private readonly IFileManager _fileManager;
        private readonly ICrawlService _crawlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadApiSample"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="renditionManager"><see cref="IRenditionManager"/> instance.</param>
        /// <param name="backgroundTaskManager"><see cref="IBackgroundTaskManager"/> instance.</param>
        /// <param name="crawlService"><see cref="ICrawlService"/> instance.</param>
        /// <param name="fileManager"><see cref="IFileManager"/> instance.</param>
        public DownloadApiSample(
            ILogger<DownloadApiSample> logger,
            IRenditionManager renditionManager,
            IBackgroundTaskManager backgroundTaskManager,
            ICrawlService crawlService,
            IFileManager fileManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _renditionManager = renditionManager ?? throw new ArgumentNullException(nameof(renditionManager));
            _backgroundTaskManager = backgroundTaskManager ?? throw new ArgumentNullException(nameof(backgroundTaskManager));
            _crawlService = crawlService ?? throw new ArgumentNullException(nameof(crawlService));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        /// <summary>
        /// Takes first available asset and download it.
        /// </summary>
        /// <returns>Action result.</returns>
        public async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                // Get first available asset to download.
                var asset = await _crawlService.FirstAvailableAssetAsync();

                if (asset == null)
                {
                    return "No assets to download.";
                }

                // Get rendition Uri to download.
                var assetRendition = asset.Renditions.First();

                // Submit rendition task.
                var downloadTaskDetails = await _renditionManager.SubmitRenditionAsync(assetRendition.Href);

                var requestedTaskInfo = new RequestedTaskInfo
                {
                    TaskId = downloadTaskDetails.Href,
                    Type = TaskType.UploadStatus
                };

                BackgroundTaskResult taskResult;
                do
                {
                    // Wait until the rendition is ready to be downloaded.
                    taskResult = await _backgroundTaskManager.GetTaskStatusAsync(requestedTaskInfo);
                }
                while (taskResult.Status != BackgroundTaskStatus.ReadyToDownload);

                var outputFilePath = Path.Combine(@"C:\temp\", asset.Filename);

                // Save downloaded rendition to file.
                await _fileManager.SaveFileAsync(((AssetReadyToBeDownloadedResult)taskResult).Asset, outputFilePath);

                Console.WriteLine($"File downloaded by the next path: {outputFilePath}");

                // Open download folder.
                Process.Start("explorer.exe", Path.GetDirectoryName(outputFilePath));
            }
            catch (Exception ex)
            {
                return $"Something went wrong: {ex.Message}";
            }

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            return "Success";
        }
    }
}