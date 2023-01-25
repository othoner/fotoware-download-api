using UploadAPI.Model;

namespace UploadAPI
{
    public interface IInputHandler
    {
        UploadDetails GetInputFromUser();
    }
}