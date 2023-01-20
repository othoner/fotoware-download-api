using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DownloadAPI
{
    internal class DownloadAPIApplication
    {
        private readonly ILogger _logger;
        private readonly IAuthentication _authentication;
        private readonly IDownload _download;
        //private readonly IInputHandler _inputHandler;

        public DownloadAPIApplication(ILogger<DownloadAPIApplication> logger, IAuthentication authentication, IDownload download)
        {
            _logger = logger;
            _authentication = authentication;
            _download = download;
        }

        public string Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);
            try
            {
                var renditionUrl = "https://oyvindthoner.fotoware.cloud/fotoweb/archives/5002-Photos/Folder%2019/Created%20on%20upload/nweweq.jpg.info/__renditions/ORIGINAL";
                var fileName = "nweweq.jpg";

                var accessToken = _authentication.GetAccessToken();
                //_download.GetAsset(accessToken);
                var downloadTaskDetails = _download.CreateDownloadTask(accessToken, renditionUrl);
                var responseString = _download.DownloadAsset(accessToken, downloadTaskDetails.Href);


                var location = _download.SaveFile(responseString.Result, fileName);
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
