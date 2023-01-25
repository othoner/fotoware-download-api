using UploadAPI.Model;

namespace UploadAPI
{
    public class InputHandler : IInputHandler
    {
        public UploadDetails GetInputFromUser()
        {
            Console.WriteLine("Enter full file path to upload file:");
            var fullFilePath = Console.ReadLine();
            Console.WriteLine("Enter destination path in your DAM:");
            var destinationPath = Console.ReadLine();
            Console.WriteLine("Enter the folder to upload to in your DAM (leave blank if no folder):");
            var folder = Console.ReadLine();
            Console.WriteLine("Enter a title for the asset:");
            var title = Console.ReadLine();
            Console.WriteLine("Enter a comment to add to the asset (leave blank if no comment):");
            var comment = Console.ReadLine();

            return new UploadDetails
            {
                FullFilePath = fullFilePath,
                Desitnation = destinationPath,
                Folder = folder == null || folder.Trim() == "" ? null : folder,
                Title = title,
                Comment = comment == null || comment.Trim() == "" ? null : comment,
            };
        }
    }
}
