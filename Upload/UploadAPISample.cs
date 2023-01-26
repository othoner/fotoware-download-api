using FWClient.Core.BackgroundTasks;
using FWClient.Core.Uploads;
using Microsoft.Extensions.Logging;
using Upload.UserInput;
using Task = System.Threading.Tasks.Task;

namespace Upload
{
    internal class UploadAPISample
    {
        private readonly ILogger _logger;
        private readonly IUploadManager _uploadManager;
        private readonly IInputHandler _inputHandler;
        private readonly IBackgroundTaskManager _backgroundTaskManager;

        public UploadAPISample(ILogger<UploadAPISample> logger, IUploadManager uploadManager, IInputHandler inputHandler, IBackgroundTaskManager backgroundTaskManager)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._uploadManager = uploadManager ?? throw new ArgumentNullException(nameof(uploadManager));
            this._inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));
            this._backgroundTaskManager = backgroundTaskManager ?? throw new ArgumentNullException(nameof(backgroundTaskManager));
        }

        public async Task<string> Run()
        {
            this._logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                var uploadDetails = this._inputHandler.GetInputFromUser();

                var fileLength = new FileInfo(uploadDetails.FullFilePath).Length;
                var uploadTaskRequest = new UploadTaskRequest
                {
                    Destination = uploadDetails.Desitnation,
                    Folder = uploadDetails.Folder,
                    FileName = Path.GetFileName(uploadDetails.FullFilePath), //fileName,
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

                var uploadTaskResponse = await this._uploadManager.CreateUploadTaskAsync(uploadTaskRequest);

                var requestedTaskInfo = new RequestedTaskInfo
                {
                    TaskId = uploadTaskResponse.Id,
                    Type = TaskType.UploadStatus
                };

                var fileUploadTask = this._uploadManager.UploadFileAsync(uploadDetails.FullFilePath, uploadTaskResponse.ChunkSize, uploadTaskResponse.Id);
                var trackTaskStatus = this.TrackTaskStatus(requestedTaskInfo);

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

            this._logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            return "Success";
        }

        private async Task<UploadStatus?> TrackTaskStatus(RequestedTaskInfo requestedTaskInfo)
        {
            UploadStatus uploadStatus;
            do
            {
                var taskResult = await this._backgroundTaskManager.GetTaskStatusAsync(requestedTaskInfo);
                if (taskResult.Status != BackgroundTaskStatus.UploadStatus)
                {
                    return null;
                }

                uploadStatus = ((UploadTaskStatusResult)taskResult).UploadStatus;

                Console.WriteLine(uploadStatus.Task.Status == UploadingTaskStatuses.AwaitingData ? "Awaiting Data...." : "Uploading....");

                await Task.Delay(TimeSpan.FromMilliseconds(uploadStatus.Job.Updates?.Frequency ?? 1000));
            } while (uploadStatus.Task.Status != UploadingTaskStatuses.Done &&
                     uploadStatus.Task.Status != UploadingTaskStatuses.Failed);

            return uploadStatus;
        }
    }
}
