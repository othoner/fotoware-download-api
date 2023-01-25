using UploadAPI.Model;

namespace UploadAPI
{
    public interface IUpload
    {
        UploadTaskResponse CreateUploadTask(string accessToken, UploadDetails uploadDetails);
        StatusResult GetUploadStatus(string accessToken, string uploadId);
        void UploadFile(string accessToken, UploadDetails uploadDetails, UploadTaskResponse uploadTaskResponse);
    }
}