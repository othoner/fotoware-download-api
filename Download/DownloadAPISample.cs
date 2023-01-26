using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Download.Crawl;
using Download.FileManager;
using FWClient.Core.BackgroundTasks;
using FWClient.Core.Renditions;
using FWClient.Core.Uploads;
using Microsoft.Extensions.Logging;

namespace Download
{
    internal class DownloadAPISample
    {
        private readonly ILogger _logger;
        private readonly IRenditionManager _renditionManager;
        private readonly IBackgroundTaskManager _backgroundTaskManager;
        private readonly IFileManager _fileManager;
        private readonly ICrawlService _crawlService;

        public DownloadAPISample(
            ILogger<DownloadAPISample> logger,
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

        public async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                var asset = await _crawlService.FirstAvailableAssetAsync();

                if (asset == null)
                {
                    return "No assets to download.";
                }

                var assetRendition = asset.Renditions.First();

                var downloadTaskDetails = await _renditionManager.SubmitRenditionAsync(assetRendition.Href);

                var requestedTaskInfo = new RequestedTaskInfo
                {
                    TaskId = downloadTaskDetails.Href,
                    Type = TaskType.UploadStatus
                };

                BackgroundTaskResult taskResult;
                do
                {
                    taskResult = await _backgroundTaskManager.GetTaskStatusAsync(requestedTaskInfo);
                } while (taskResult.Status != BackgroundTaskStatus.ReadyToDownload);

                var outputFilePath = Path.Combine(@"C:\temp\", asset.Filename);

                await _fileManager.SaveFileAsync(((AssetReadyToBeDownloadedResult)taskResult).Asset, outputFilePath);

                Console.WriteLine($"File downloaded by the next path: {outputFilePath}");

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