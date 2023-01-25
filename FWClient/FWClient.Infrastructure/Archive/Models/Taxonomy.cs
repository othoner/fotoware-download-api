using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    public class Taxonomy
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pluralName")]
        public string PluralName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hasChildren")]
        public bool? HasChildren { get; set; }

        [JsonProperty("field")]
        public int? Field { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("taxonomyHref")]
        public string TaxonomyHref { get; set; }

        [JsonProperty("items")]
        public List<TaxonomyItem> Items { get; set; }
    }
}