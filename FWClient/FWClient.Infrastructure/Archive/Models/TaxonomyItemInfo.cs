using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FWClient.Core.Archive
{
    public class TaxonomyItemInfo
    {
        [JsonProperty("alt_orders")]
        public List<AltOrder> AltOrders { get; set; }

        [JsonProperty("alertHref")]
        public AlertHref? AlertHref { get; set; }

        [JsonProperty("archived")]
        public string Archived { get; set; }

        [JsonProperty("assets")]
        public CollectionList Assets { get; set; }

        [JsonProperty("assetCount")]
        public int AssetCount { get; set; }

        [JsonProperty("canBeArchived")]
        public bool CanBeArchived { get; set; }

        [JsonProperty("canBeDeleted")]
        public bool CanBeDeleted { get; set; }

        [JsonProperty("canCopyTo")]
        public bool CanCopyTo { get; set; }

        [JsonProperty("canCreateFolders")]
        public bool CanCreateFolders { get; set; }

        [JsonProperty("canHaveChildren")]
        public bool CanHaveChildren { get; set; }

        [JsonProperty("canIngestToChildren")]
        public bool CanIngestToChildren { get; set; }

        [JsonProperty("canMoveTo")]
        public bool CanMoveTo { get; set; }

        [JsonProperty("canSelect")]
        public bool CanSelect { get; set; }

        [JsonProperty("canUploadTo")]
        public bool CanUploadTo { get; set; }

        [JsonProperty("children")]
        public Children Children { get; set; }

        [JsonProperty("childCount")]
        public int? ChildCount { get; set; }

        [JsonProperty("clearSearch")]
        public ClearSearch ClearSearch { get; set; }

        [JsonProperty("create")]
        public List<Create> Create { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("dataTemplate")]
        public string DataTemplate { get; set; }

        [JsonProperty("deleted")]
        public string Deleted { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("edit")]
        public bool? Edit { get; set; }

        [JsonProperty("hasChildren")]
        public bool? HasChildren { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("isFolderNavigationEnabled")]
        public bool? IsFolderNavigationEnabled { get; set; }

        [JsonProperty("isLinkCollection")]
        public bool? IsLinkCollection { get; set; }

        [JsonProperty("isSearchable")]
        public bool? IsSearchable { get; set; }

        [JsonProperty("isSelectable")]
        public bool? IsSelectable { get; set; }

        [JsonProperty("isSmartFolderNavigationEnabled")]
        public bool? IsSmartFolderNavigationEnabled { get; set; }

        [JsonProperty("matchingHref")]
        public string MatchingHref { get; set; }

        [JsonProperty("metadataEditor")]
        public MetadataEditor MetadataEditor { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderRootHref")]
        public string OrderRootHref { get; set; }

        [JsonProperty("originalURL")]
        public string OriginalURL { get; set; }

        [JsonProperty("permissions", ItemConverterType = typeof(StringEnumConverter))]
        public List<Permissions> Permissions { get; set; }

        [JsonProperty("pin")]
        public Pin Pin { get; set; }

        [JsonProperty("posterAsset")]
        public string PosterAsset { get; set; }

        [JsonProperty("posterImages")]
        public List<PosterImage> PosterImages { get; set; }

        [JsonProperty("propertyValidations")]
        public List<PropertyValidation> PropertyValidations { get; set; }

        [JsonProperty("props")]
        public Props Props { get; set; }

        [JsonProperty("reorder")]
        public Reorder Reorder { get; set; }

        [JsonProperty("searchQuery")]
        public string SearchQuery { get; set; }

        [JsonProperty("searchString")]
        public string SearchString { get; set; }

        [JsonProperty("searchURL")]
        public string SearchURL { get; set; }

        [JsonProperty("smartFolderHeader")]
        public string SmartFolderHeader { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("taxonomies")]
        public List<Taxonomy> Taxonomies { get; set; }

        [JsonProperty("uploadHref")]
        public string UploadHref { get; set; }

        // Not Documented fields
        [JsonProperty("field")]
        public int? Field { get; set; }

        [JsonProperty("taxonomyHref")]
        public string TaxonomyHref { get; set; }

        [JsonProperty("acl")]
        public List<string> Acl { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("customSearch")]
        public string CustomSearch { get; set; }
    }
}