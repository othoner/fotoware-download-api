using FWClient.Core.BackgroundTasks;
using FWClient.Core.Renditions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using FWClient.Core.Archive;

namespace DownloadAPI
{
    internal class DownloadAPIApplication
    {
        private readonly ILogger _logger;
        private readonly IRenditionManager _renditionManager;
        private readonly IBackgroundTaskManager _backgroundTaskManager;
        private readonly IArchiveManager _archiveManager;

        //private readonly IInputHandler _inputHandler;

        public DownloadAPIApplication(ILogger<DownloadAPIApplication> logger, IRenditionManager renditionManager, IBackgroundTaskManager backgroundTaskManager, IArchiveManager archiveManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _renditionManager = renditionManager ?? throw new ArgumentNullException(nameof(renditionManager));
            _backgroundTaskManager = backgroundTaskManager ?? throw new ArgumentNullException(nameof(backgroundTaskManager));
            _archiveManager = archiveManager ?? throw new ArgumentNullException(nameof(archiveManager));
        }

        public async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                ////var renditionUrl = "https://oyvindthoner.fotoware.cloud/fotoweb/archives/5002-Photos/Folder%2019/Created%20on%20upload/nweweq.jpg.info/__renditions/ORIGINAL";
                var renditionUrl = "fotoweb/archives/5002-Photos/Folder%2019/Created%20on%20upload/nweweq.jpg.info/__renditions/ORIGINAL";
                var fileName = "nweweq.jpg";

                //_downloadService.GetAsset(accessToken);
                var archives = await _archiveManager.GetAll();
                var firstAvailableArchive = archives.Data.FirstOrDefault();

                if (firstAvailableArchive == null)
                {
                    return "No archives available.";
                }

                var firstAvailableAsset = firstAvailableArchive.Assets.Data.FirstOrDefault();

                var downloadTaskDetails = await _renditionManager.SubmitRenditionAsync(renditionUrl);
                var responseString = await _backgroundTaskManager.GetTaskStatusAsync(downloadTaskDetails.Href);

                ////var location = _downloadService.SaveFile(Array.Empty<byte>(), fileName);
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
