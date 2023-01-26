using FWClient.Core.BackgroundTasks;
using FWClient.Core.Uploads;
using Microsoft.Extensions.Logging;
using Upload.UserInput;
using Task = System.Threading.Tasks.Task;

namespace Upload
{
    /// <summary>
    /// Upload asset sample.
    /// </summary>
    internal class UploadApiSample
    {
        private readonly ILogger _logger;
        private readonly IUploadManager _uploadManager;
        private readonly IInputHandler _inputHandler;
        private readonly IBackgroundTaskManager _backgroundTaskManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadApiSample"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="uploadManager"><see cref="IUploadManager"/> instance.</param>
        /// <param name="inputHandler"><see cref="IInputHandler"/> instance.</param>
        /// <param name="backgroundTaskManager"><see cref="IBackgroundTaskManager"/> instance.</param>
        public UploadApiSample(
            ILogger<UploadApiSample> logger,
            IUploadManager uploadManager,
            IInputHandler inputHandler,
            IBackgroundTaskManager backgroundTaskManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadManager = uploadManager ?? throw new ArgumentNullException(nameof(uploadManager));
            _inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));
            _backgroundTaskManager = backgroundTaskManager ?? throw new ArgumentNullException(nameof(backgroundTaskManager));
        }

        /// <summary>
        /// Upload asset to archive.
        /// </summary>
        /// <returns>Action result.</returns>
        public async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                // Get upload details.
                var uploadDetails = _inputHandler.GetInputFromUser();

                var fileLength = new FileInfo(uploadDetails.FullFilePath).Length;
                var uploadTaskRequest = new UploadTaskRequest
                {
                    Destination = uploadDetails.Desitnation,
                    Folder = uploadDetails.Folder,
                    FileName = Path.GetFileName(uploadDetails.FullFilePath),
                    FileSize = fileLength,
                    CheckoutId = null,
                    HasXmp = false,
                    IgnoreMetadata = false,
                    Metadata = new MetaData
                    {
                        Fields = new List<MetaDataField>
                        {
                            new () { Id = 5, Action = "Add", Value = new List<string> { uploadDetails.Title } }
                        },
                        Attributes = new List<KeyValuePair<string, string>>()
                    },
                    Comment = uploadDetails.Comment
                };

                // Register upload task.
                var uploadTaskResponse = await _uploadManager.CreateUploadTaskAsync(uploadTaskRequest);

                var requestedTaskInfo = new RequestedTaskInfo
                {
                    TaskId = uploadTaskResponse.Id,
                    Type = TaskType.UploadStatus
                };

                // Upload file as chunks async.
                var fileUploadTask = _uploadManager.UploadFileAsync(uploadDetails.FullFilePath, uploadTaskResponse.ChunkSize, uploadTaskResponse.Id);

                // Start track status task.
                var trackTaskStatus = TrackTaskStatus(requestedTaskInfo);

                // Wait until file is uploaded.
                await Task.WhenAll(fileUploadTask, trackTaskStatus);

                if (trackTaskStatus.Result == null)
                {
                    return "Upload task not found or wrong id provided.";
                }

                Console.WriteLine($"Uploaded task completed, status: {trackTaskStatus.Result.Task.Status:G}");
            }
            catch (Exception ex)
            {
                return $"Something went wrong: {ex.Message}";
            }

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            return "Success";
        }

        private async Task<UploadStatus?> TrackTaskStatus(RequestedTaskInfo requestedTaskInfo)
        {
            UploadStatus uploadStatus;
            do
            {
                var taskResult = await _backgroundTaskManager.GetTaskStatusAsync(requestedTaskInfo);
                if (taskResult.Status != BackgroundTaskStatus.UploadStatus)
                {
                    return null;
                }

                uploadStatus = ((UploadTaskStatusResult)taskResult).UploadStatus;

                Console.WriteLine(uploadStatus.Task.Status == UploadingTaskStatuses.AwaitingData
                    ? "Awaiting Data...."
                    : "Uploading....");

                await Task.Delay(TimeSpan.FromMilliseconds(uploadStatus.Job.Updates?.Frequency ?? 1000));
            }
            while (uploadStatus.Task.Status != UploadingTaskStatuses.Done &&
                     uploadStatus.Task.Status != UploadingTaskStatuses.Failed);

            return uploadStatus;
        }
    }
}