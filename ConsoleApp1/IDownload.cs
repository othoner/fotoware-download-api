using DownloadAPI.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadAPI
{
    public interface IDownload
    {
        DownloadTaskResponse CreateDownloadTask(string accessToken, string renditionUrl);
        Task<byte[]> DownloadAsset(string accessToken, string backgroundTaskUrl);
        Task<string> SaveFile(byte[] fileData, string fileName);

        void GetAsset(string accessToken);

        void UploadToInriver(HttpResponseMessage response, string fileName);
    }
}