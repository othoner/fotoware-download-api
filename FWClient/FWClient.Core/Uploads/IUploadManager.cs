namespace FWClient.Core.Uploads
{
    public interface IUploadManager
    {
        Task<UploadTaskResponse> CreateUploadTaskAsync(UploadTaskRequest href);
        Task UploadFileAsync(string fullFilePath, int chunkSize, string uploadId);
    }
}