using FWClient.Core.Archive;
using Newtonsoft.Json;

namespace FWClient.Core.Assets
{
    public class Asset : CollectionItemBase
    {
        [JsonProperty("archiveHREF")]
        public string ArchiveHREF { get; set; }

        [JsonProperty("archiveId")]
        public int ArchiveId { get; set; }

        [JsonProperty("linkstance")]
        public string Linkstance { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("filesize")]
        public int Filesize { get; set; }

        [JsonProperty("uniqueid")]
        public string Uniqueid { get; set; }

        [JsonProperty("pincount")]
        public int Pincount { get; set; }

        [JsonProperty("previewcount")]
        public int Previewcount { get; set; }

        [JsonProperty("downloadcount")]
        public int Downloadcount { get; set; }

        [JsonProperty("workflowcount")]
        public int Workflowcount { get; set; }

        [JsonProperty("metadataeditcount")]
        public int Metadataeditcount { get; set; }

        [JsonProperty("revisioncount")]
        public int Revisioncount { get; set; }

        [JsonProperty("doctype")]
        public string Doctype { get; set; }

        [JsonProperty("renditions")]
        public List<AssetRendition> Renditions { get; set; }

        [JsonProperty("previewToken")]
        public object PreviewToken { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("physicalFileId")]
        public string PhysicalFileId { get; set; }
    }
}