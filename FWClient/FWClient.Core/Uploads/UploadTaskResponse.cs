namespace FWClient.Core.Uploads
{
    public class UploadTaskResponse
    {
        public string Id { get; set; }

        public int ChunkSize { get; set; }

        public int NumChunks { get; set; }

        public bool HasXmp { get; set; }
    }
}