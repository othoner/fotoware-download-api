namespace UploadAPI.Model
{
    public class StatusResult
    {
        public string Status { get; set; }
        public AssetResult AssetResult { get; set; }
        public StatusError Error { get; set; }

    }
}
