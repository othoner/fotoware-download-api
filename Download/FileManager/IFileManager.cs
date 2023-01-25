using System.Threading.Tasks;

namespace Download.FileManager
{
    internal interface IFileManager
    {
        Task SaveFileAsync(byte[] fileData, string filePath);
    }
}