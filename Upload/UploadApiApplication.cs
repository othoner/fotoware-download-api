using Microsoft.Extensions.Logging;
using UploadAPI.Model;

namespace UploadAPI
{
    internal class UploadAPIApplication
    {
        private readonly ILogger _logger;
        private readonly IAuthentication _authentication;
        private readonly IUpload _upload;
        private readonly IInputHandler _inputHandler;

        public UploadAPIApplication(ILogger<UploadAPIApplication> logger, IAuthentication authentication, IUpload upload, IInputHandler inputHandler)
        {
            _logger = logger;
            _authentication = authentication;
            _upload = upload;
            _inputHandler = inputHandler;
        }

        public string Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            try
            {
                var uploadDetails = _inputHandler.GetInputFromUser();
                var accessToken = _authentication.GetAccessToken();
                var uploadTaskResponse = _upload.CreateUploadTask(accessToken, uploadDetails);
                _upload.UploadFile(accessToken, uploadDetails, uploadTaskResponse);
                var uploadStatus = "";
                StatusResult status = null;
                while (uploadStatus != "done" && uploadStatus != "failed")
                {
                    Console.WriteLine("Uploading....");
                    status = _upload.GetUploadStatus(accessToken, uploadTaskResponse.Id);
                    uploadStatus = status.Status;
                    Thread.Sleep(5000);
                }
                Console.WriteLine($"Uploaded task completetd, status: {status.Status}");
            }
            catch(Exception ex)
            {
                return $"Something went wrong: {ex.Message}";
            }

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            return "Success";
        }
    }
}
