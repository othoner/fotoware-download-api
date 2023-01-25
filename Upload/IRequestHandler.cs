
namespace UploadAPI
{
    public interface IRequestHandler
    {
        string GetRequestId(HttpResponseMessage responseMessage);
    }
}