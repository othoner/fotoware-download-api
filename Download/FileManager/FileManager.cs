using System.IO;
using System.Threading.Tasks;

namespace Download.FileManager
{
    public class FileManager : IFileManager
    {
        public async Task SaveFileAsync(byte[] fileData, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await stream.WriteAsync(fileData, 0, fileData.Length);
            }
        }
    }
}