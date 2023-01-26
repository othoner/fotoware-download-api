namespace FWClient.Core.Uploads
{
    public class UploadTaskRequest
    {
        public string Destination { get; set; }
        public string? Folder { get; set; }
        public string FileName { get; set; }
        public bool HasXmp { get; set; }
        public long FileSize { get; set; }
        public string CheckoutId { get; set; }
        public MetaData Metadata { get; set; }
        public bool IgnoreMetadata { get; set; }
        public string Comment { get; set; }
    }
}
