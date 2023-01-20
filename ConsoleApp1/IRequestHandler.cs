using System.Net.Http;

namespace DownloadAPI
{
    public interface IRequestHandler
    {
        string GetRequestId(HttpResponseMessage responseMessage);
    }
}
